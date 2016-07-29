using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DS.Kids.Model.Repositories;
using DS.Kids.Model.Validations;

namespace DS.Kids.Model.Services
{
    public class Diario : IDiario
    {
        private readonly ICriancas _crianca;
        private readonly IRefeicoesGrupos _refeicoesGrupos;
        private readonly IRefeicoesDiarios _refeicoesDiarios;

        /// <summary>
        /// Construtor padrão
        /// </summary>
        /// <param name="criancas">Repositório de Crianças</param>
        /// <param name="refeicoesDiarios">Repositório de refeições do diário</param>
        /// <param name="refeicoesGrupos">Repositório de grupos de refeições</param>
        public Diario(IRefeicoesDiarios refeicoesDiarios, IRefeicoesGrupos refeicoesGrupos, ICriancas criancas)
        {
            Throw.IfIsNull(criancas);
            Throw.IfIsNull(refeicoesGrupos);
            Throw.IfIsNull(refeicoesDiarios);

            _crianca = criancas;
            _refeicoesGrupos = refeicoesGrupos;
            _refeicoesDiarios = refeicoesDiarios;
        }

        public async Task<Result<Model.Diario>> ObterPorIdDataAsync(int idCrianca, DateTime dataDiario)
        {
            var crianca = await _crianca.ObterPorIdAsync(idCrianca);

            if (crianca == null)
            {
                return new Result<Model.Diario>(ResultCodes.CriancaNaoEncontrada);
            }
            if (dataDiario.Date > DateTime.Now.Date)
            {
                return new Result<Model.Diario>(ResultCodes.DataInvalida);
            }
            if (dataDiario < crianca.DataNascimento.AddYears(2) || dataDiario > crianca.DataNascimento.AddYears(11))
            {
                return new Result<Model.Diario>(ResultCodes.DataNascimentoInvalidaCrianca);
            }

            var listaRefeicaoDiario = await _refeicoesDiarios.ObterPorIdDataAsync(idCrianca, dataDiario);

            var result = new Result<Model.Diario>(new Model.Diario
                                                         {
                                                             CafeDaManha = ObterDiarioPorTipoRefeicao(listaRefeicaoDiario, crianca, TipoRefeicao.CafeDaManha),
                                                             LancheDaManha = ObterDiarioPorTipoRefeicao(listaRefeicaoDiario, crianca, TipoRefeicao.LancheDaManha),
                                                             Almoco = ObterDiarioPorTipoRefeicao(listaRefeicaoDiario, crianca, TipoRefeicao.Almoco),
                                                             LancheDaTarde = ObterDiarioPorTipoRefeicao(listaRefeicaoDiario, crianca, TipoRefeicao.LancheDaTarde),
                                                             Jantar = ObterDiarioPorTipoRefeicao(listaRefeicaoDiario, crianca, TipoRefeicao.Jantar),
                                                             LancheDaNoite = ObterDiarioPorTipoRefeicao(listaRefeicaoDiario, crianca, TipoRefeicao.LancheDaNoite)
                                                         });

            return result;
        }

        private static RefeicaoDiario ObterDiarioPorTipoRefeicao(IList<RefeicaoDiario> listaRefeicaoDiario, Model.Crianca crianca, TipoRefeicao tipoRefeicao)
        {
            var refeicaoDiario = listaRefeicaoDiario.FirstOrDefault(x => x.IdTipoRefeicao == (int)tipoRefeicao);

            if (refeicaoDiario == null)
            {
                refeicaoDiario = new RefeicaoDiario(crianca.DataNascimento, tipoRefeicao);
            }
            else
            {
                refeicaoDiario.TipoRefeicao = tipoRefeicao;

                foreach (var refeicaoGrupo in RefeicaoDiario.DefaultRefeicoesGrupo(crianca.DataNascimento, tipoRefeicao))
                {
                    var refeicaoGrupoDiario = refeicaoDiario.RefeicoesGrupos.FirstOrDefault(r => r.IdGrupo == (int)refeicaoGrupo.TipoGrupoRefeicao);
                    if (refeicaoGrupoDiario == null)
                    {
                        refeicaoDiario.RefeicoesGrupos.Add(refeicaoGrupo);
                    }
                    else
                    {
                        refeicaoGrupoDiario.TipoGrupoRefeicao = refeicaoGrupo.TipoGrupoRefeicao;
                        refeicaoGrupoDiario.Sugerido = RefeicaoGrupo.GetSugerido(crianca.DataNascimento, tipoRefeicao, refeicaoGrupoDiario.TipoGrupoRefeicao);

                        if (refeicaoGrupoDiario.Alimentos.Any())
                        {
                            var mesesDeIdade = crianca.MesesDeIdade;

                            Func<AlimentoMedidaFaixaEtaria, bool> faixaEtariaValida = x => x.FaixasEtaria.MesesDeIdadeInicial <= mesesDeIdade &&
                                                                                            mesesDeIdade <= x.FaixasEtaria.MesesDeIdadeFinal;

                            foreach (var alimento in refeicaoGrupoDiario.Alimentos)
                            {
                                alimento.AlimentosMedidasFaixasEtarias = alimento.AlimentosMedidasFaixasEtarias.Where(faixaEtariaValida).ToList();

                                // Removendo propriedades desnecessárias no retorno do serviço
                                var alimentoMedidaFaixaEtaria = alimento.AlimentosMedidasFaixasEtarias.FirstOrDefault();
                                if (alimentoMedidaFaixaEtaria != null)
                                {
                                    alimentoMedidaFaixaEtaria.FaixasEtaria = null;
                                }
                                alimento.RefeicoesGrupos = null;
                                alimento.RefeicoesItens = null;
                            }
                        }
                    }
                }
            }

            return refeicaoDiario;
        }

        public async Task<Result<DiarioDTO>> AtualizarAsync(DiarioDTO diarioDTO)
        {
            var refeicaoDiario =
                await
                    _refeicoesDiarios.ObterPorIdDataRefeicaoAsync(diarioDTO.IdCrianca, diarioDTO.Data,
                        diarioDTO.IdTipoRefeicao);
            if (refeicaoDiario == null)
            {
                try
                {
                    refeicaoDiario = new RefeicaoDiario
                    {
                        DataCriacao = diarioDTO.Data,
                        IdCrianca = diarioDTO.IdCrianca,
                        IdTipoRefeicao = (int) diarioDTO.IdTipoRefeicao
                    };

                    await _refeicoesDiarios.InserirAsync(refeicaoDiario);
                }
                catch (DuplicateEntityException)
                {
                    return new Result<DiarioDTO>(diarioDTO, ResultCodes.RefeicaoDuplicada);
                }
            }

            var refeicaoGrupo = refeicaoDiario.RefeicoesGrupos.FirstOrDefault(g => g.IdGrupo == (int) diarioDTO.IdGrupo);
            if (refeicaoGrupo == null)
            {
                refeicaoGrupo = new RefeicaoGrupo
                {
                    IdGrupo = (int) diarioDTO.IdGrupo,
                    IdRefeicao = refeicaoDiario.IdRefeicao
                };

                await _refeicoesGrupos.InserirAsync(refeicaoGrupo);
            }
            else if (diarioDTO.IdAlimento != null)
            {
                // Muda para um objeto do mesmo context do _refeicoesGrupos
                refeicaoGrupo = await _refeicoesGrupos.ObterPorIdAsync(refeicaoGrupo.IdRefeicaoGrupo);
            }

            diarioDTO.IdRefeicao = refeicaoDiario.IdRefeicao;
            diarioDTO.IdRefeicaoGrupo = refeicaoGrupo.IdRefeicaoGrupo;

            if (diarioDTO.IdAlimento != null)
            {
                try
                {
                    await _refeicoesGrupos.AdicionarAlimento(diarioDTO.IdAlimento.Value, refeicaoGrupo);
                }
                catch (DuplicateEntityException)
                {
                    return new Result<DiarioDTO>(diarioDTO, ResultCodes.AlimentosRefeicaoDuplicado);
                }
            }
            else if (!diarioDTO.Checked && !refeicaoGrupo.Alimentos.Any())
            {
                diarioDTO.IdRefeicaoGrupo = null;
                await _refeicoesGrupos.RemoverAsync(refeicaoGrupo);

                if (!refeicaoDiario.RefeicoesGrupos.Any())
                {
                    diarioDTO.IdRefeicao = null;
                    await _refeicoesDiarios.RemoverAsync(refeicaoDiario);
                }
            }

            return new Result<DiarioDTO>(diarioDTO);
        }

        public async Task<Result<DiarioDTO>> RemoverAlimentoAsync(int idRefeicaoGrupo, int idAlimento)
        {
            var refeicaoGrupo = await _refeicoesGrupos.ObterPorIdAsync(idRefeicaoGrupo);

            if (refeicaoGrupo == null)
                return new Result<DiarioDTO>(null, ResultCodes.RefeicaoNaoEncontrada);

            await _refeicoesGrupos.RemoverAlimento(idAlimento, refeicaoGrupo);

            var refeicaoDiario = refeicaoGrupo.RefeicaoDiario;

            DiarioDTO diarioDTO = null;

            if (!refeicaoGrupo.Alimentos.Any())
            {
                await _refeicoesGrupos.RemoverAsync(refeicaoGrupo);

                diarioDTO = new DiarioDTO
                {
                    IdRefeicaoGrupo = 0
                };

                if (!refeicaoDiario.RefeicoesGrupos.Any())
                {
                    diarioDTO.IdRefeicao = 0;

                    await _refeicoesDiarios.RemoverAsync(refeicaoDiario);
                }
            }

            return new Result<DiarioDTO>(diarioDTO);
        }

    }
}
