using System;
using System.Linq;
using System.Threading.Tasks;

namespace DS.Kids.Model.Services
{
    /// <summary>
    /// Serviço de Criança
    /// </summary>
    public class Crianca : ICrianca
    {
        private readonly Repositories.ICriancas _criancas;
        private readonly Repositories.IResponsaveis _responsaveis;
        private readonly Repositories.ICrescimentos _crescimentos;

        private readonly Services.ICrescimento _crescimento;

        /// <summary>
        /// Construtor padrão
        /// </summary>
        /// <param name="criancas">Repositório de Crianças</param>
        /// <param name="responsaveis">Repositório de Responsáveis</param>
        /// <param name="crescimentos">Repositório de Crescimentos</param>
        public Crianca(Repositories.ICriancas criancas, Repositories.IResponsaveis responsaveis, Repositories.ICrescimentos crescimentos)
        {
            Throw.IfIsNull(criancas);
            Throw.IfIsNull(responsaveis);
            Throw.IfIsNull(crescimentos);

            this._criancas = criancas;
            this._responsaveis = responsaveis;
            this._crescimentos = crescimentos;

            this._crescimento = new Crescimento(this._criancas, this._crescimentos);
        }

        /// <summary>
        /// Inserir criança de forma assíncrona
        /// </summary>
        /// <param name="crianca">Criança</param>
        /// <returns>Criança inserida</returns>
        public async Task<Model.Result<Model.Crianca>> InserirAsync(Model.Crianca crianca)
        {
            Throw.IfIsNull(crianca);

            var validate = crianca.Validate();
            if (validate != Validations.ResultCodes.Success) return new Result<Model.Crianca>(validate);

            var responsavel = await this._responsaveis.ObterPorIdAsync(crianca.IdResponsavel);
            if (responsavel == null)
                return new Result<Model.Crianca>(null, Validations.ResultCodes.ResponsavelNaoEncontrado);

            validate = responsavel.ValidarInsercaoCrianca(crianca);
            if (validate != Validations.ResultCodes.Success) return new Result<Model.Crianca>(validate);

            await this._criancas.InserirAsync(crianca);
            var result = await this.InserirCrescimentoAsync(crianca);
            crianca.Crescimentos.Add(result.Data);
            return new Result<Model.Crianca>(crianca);
        }

        /// <summary>
        /// Atualiza uma Criança
        /// </summary>
        /// <param name="crianca"></param>
        /// <returns></returns>
        public async Task<Result<Model.Crianca>> AtualizarAsync(Model.Crianca crianca)
        {
            Throw.IfIsNull(crianca);

            var validate = crianca.Validate();
            if (validate != Validations.ResultCodes.Success) return new Result<Model.Crianca>(validate);

            var responsavel = await this._responsaveis.ObterPorIdAsync(crianca.IdResponsavel);
            if (responsavel == null)
                return new Result<Model.Crianca>(crianca, Validations.ResultCodes.ResponsavelNaoEncontrado);

            var criancaAtual = await this._criancas.ObterPorIdAsync(crianca.IdCrianca);
            validate = criancaAtual.ValidarAtualizacaoCrianca(crianca);
            if (validate != Validations.ResultCodes.Success) return new Result<Model.Crianca>(validate);

            await this._criancas.AtualizarAsync(crianca);
            return new Result<Model.Crianca>(crianca);
        }

        /// <summary>
        /// Exclui uma Criança
        /// </summary>
        /// <param name="id">Id da Criança</param>
        /// <returns>Resultado da Ação</returns>
        public async Task<Model.Result> ExcluirAsync(int id)
        {
            Throw.IfLessThanOrEqZero(id);
            await this._criancas.InativarAsync(id);
            return Result.SUCCESS;
        }

        /// <summary>
        /// Inseri informação de crescimento da criança
        /// </summary>
        /// <param name="crianca">Criança</param>
        /// <returns>Tipo de Crescimento</returns>
        private async Task<Result<Model.Crescimento>> InserirCrescimentoAsync(Model.Crianca crianca)
        {
            Throw.IfIsNull(crianca);

            var pesoAltura = new Model.PesoAltura
            {
                Peso = crianca.PesoInicial,
                Altura = crianca.AlturaInicial,
                IdCrianca = crianca.IdCrianca
            };

            return await this._crescimento.InserirAsync(pesoAltura);
        }
    }
}