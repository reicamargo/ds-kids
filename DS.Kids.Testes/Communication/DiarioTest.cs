using System;
using System.Collections.Generic;
using System.Linq;

using DS.Kids.Model;
using DS.Kids.Model.Communication;
using DS.Kids.Model.Validations;
using DS.Kids.Testes.Infra;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DS.Kids.Testes.Communication
{
    [TestClass]
    public class DiarioTest
    {

        private int _idCrianca;

        [TestInitialize]
        public void Initialize()
        {
            Authorization.Singleton.KillToken();

            var expected = ResultCodes.Success;

            var responsavelResult = InserirResponsavel();
            Assert.AreEqual(expected, responsavelResult.ResultCode);

            var criancaResult = InserirCrianca(responsavelResult.Data.IdResponsavel);
            Assert.AreEqual(expected, criancaResult.ResultCode);

            _idCrianca = criancaResult.Data.IdCrianca;
        }

        [TestCleanup]
        public void Cleanup()
        {
            Authorization.Singleton.KillToken();
        }

        [TestMethod]
        public void Obter_Diario()
        {
            var result = Obter(_idCrianca);

            Assert.AreEqual(ResultCodes.Success, result.ResultCode);
        }

        [TestMethod]
        public void Atualizar_Diario_AdicionarAlimento()
        {
            var idGrupo = TipoGrupoRefeicao.AcucaresEDoces;
            var alimentos = ObterAlimentos(_idCrianca, (int)idGrupo).Data;

            var diarioDTO = Atualizar(new DiarioDTO
            {
                Data = DateTime.Now,
                IdCrianca = _idCrianca,
                IdGrupo = idGrupo,
                IdTipoRefeicao = TipoRefeicao.Almoco,
                IdAlimento = alimentos.First().IdAlimento
            });

            Assert.IsNotNull(diarioDTO.Data.IdRefeicao);
            Assert.IsNotNull(diarioDTO.Data.IdRefeicaoGrupo);
        }

        [TestMethod]
        public void Atualizar_Diario_Adicionar_Duas_Vezes_O_Mesmo_Alimento_Adiciona_Apenas_Uma_Vez()
        {
            var idGrupo = TipoGrupoRefeicao.AcucaresEDoces;
            var alimentos = ObterAlimentos(_idCrianca, (int)idGrupo).Data.ToList();

            var diarioDto = new DiarioDTO
                                {
                                    Data = DateTime.Now,
                                    IdCrianca = _idCrianca,
                                    IdGrupo = idGrupo,
                                    IdTipoRefeicao = TipoRefeicao.Almoco,
                                    IdAlimento = alimentos.First().IdAlimento
                                };
            var diarioDTO = Atualizar(diarioDto);

            Assert.IsNotNull(diarioDTO.Data.IdRefeicao);
            Assert.IsNotNull(diarioDTO.Data.IdRefeicaoGrupo);

            diarioDTO = Atualizar(diarioDto);

            Assert.IsNotNull(diarioDTO.Data.IdRefeicao);
            Assert.IsNotNull(diarioDTO.Data.IdRefeicaoGrupo);
        }

        [TestMethod]
        public void Atualizar_Diario_Adicionar_Tres_Alimentos_Diferentes()
        {
            var idGrupo = TipoGrupoRefeicao.AcucaresEDoces;
            var alimentos = ObterAlimentos(_idCrianca, (int)idGrupo).Data.ToList();
            
            var diarioDto = new DiarioDTO
            {
                Data = DateTime.Now,
                IdCrianca = _idCrianca,
                IdGrupo = idGrupo,
                IdTipoRefeicao = TipoRefeicao.Almoco,
                IdAlimento = alimentos[0].IdAlimento
            };
            var diarioDTO = Atualizar(diarioDto);
            Assert.IsNotNull(diarioDTO.Data.IdRefeicao);
            Assert.IsNotNull(diarioDTO.Data.IdRefeicaoGrupo);
            diarioDto.IdAlimento = alimentos[1].IdAlimento;
            diarioDTO = Atualizar(diarioDto);
            Assert.IsNotNull(diarioDTO.Data.IdRefeicao);
            Assert.IsNotNull(diarioDTO.Data.IdRefeicaoGrupo);
            diarioDto.IdAlimento = alimentos[3].IdAlimento;
            diarioDTO = Atualizar(diarioDto);
            Assert.IsNotNull(diarioDTO.Data.IdRefeicao);
            Assert.IsNotNull(diarioDTO.Data.IdRefeicaoGrupo);
        }

        [TestMethod]
        public void Check_Diario()
        {
            var idGrupo = TipoGrupoRefeicao.CarnesEOvos;

            var diarioDTO = Atualizar(new DiarioDTO
            {
                Data = DateTime.Now,
                IdCrianca = _idCrianca,
                IdGrupo = idGrupo,
                IdTipoRefeicao = TipoRefeicao.Almoco,
                Checked = true
            });

            Assert.IsNotNull(diarioDTO.Data.IdRefeicao);
            Assert.IsNotNull(diarioDTO.Data.IdRefeicaoGrupo);
        }

        [TestMethod]
        public void Remover_Check_Diario()
        {
            //Checa carnes e ovos
            var a = Atualizar(new DiarioDTO
            {
                Data = DateTime.Now,
                IdCrianca = _idCrianca,
                IdGrupo = TipoGrupoRefeicao.CarnesEOvos,
                IdTipoRefeicao = TipoRefeicao.Almoco,
                Checked = true
            });

            //Checa Acucares e doces
            var b = Atualizar(new DiarioDTO
            {
                Data = DateTime.Now,
                IdCrianca = _idCrianca,
                IdGrupo = TipoGrupoRefeicao.AcucaresEDoces,
                IdTipoRefeicao = TipoRefeicao.Almoco,
                Checked = true
            });

            //Remove o check de açucares e doces
            var diarioDTO = Atualizar(new DiarioDTO
            {
                Data = DateTime.Now,
                IdCrianca = _idCrianca,
                IdGrupo = TipoGrupoRefeicao.AcucaresEDoces,
                IdTipoRefeicao = TipoRefeicao.Almoco,
                Checked = false
            });

            Assert.IsNotNull(diarioDTO.Data.IdRefeicao);
            Assert.IsNull(diarioDTO.Data.IdRefeicaoGrupo);
        }

        [TestMethod]
        public void Remover_Alimento_Diario()
        {
            var idGrupo = TipoGrupoRefeicao.AcucaresEDoces;
            var idAlimento = ObterAlimentos(_idCrianca, (int)idGrupo).Data.First().IdAlimento;

            var refeicaoGrupo = Atualizar(new DiarioDTO
                                              {
                                                  Data = DateTime.Now,
                                                  IdCrianca = _idCrianca,
                                                  IdGrupo = idGrupo,
                                                  IdTipoRefeicao = TipoRefeicao.Almoco,
                                                  IdAlimento = idAlimento
                                              }).Data.IdRefeicaoGrupo;
            if(refeicaoGrupo != null)
            {
                var idRefeicaoGrupo = refeicaoGrupo.Value;

                var diarioDTO = Remover(idRefeicaoGrupo, idAlimento);

                Assert.IsNotNull(diarioDTO.Data.IdRefeicao);
                Assert.IsNotNull(diarioDTO.Data.IdRefeicaoGrupo);
                Assert.AreEqual(0, diarioDTO.Data.IdRefeicao);
                Assert.AreEqual(0, diarioDTO.Data.IdRefeicaoGrupo);
            }
        }

        private static Result<IEnumerable<Alimento>> ObterAlimentos(int idCrianca, int idGrupo)
        {
            var communication = new Alimentos();
            return communication.ObterPorGrupoAlimentar(idCrianca, idGrupo).Result;
        }

        private static Result<Diario> Obter(int idCrianca)
        {
            return Obter(idCrianca, DateTime.Now);
        }

        private static Result<Diario> Obter(int idCrianca, DateTime data)
        {
            var communication = new Diarios();
            return communication.ObterPorIdDataAsync(idCrianca, data).Result;
        }

        private static Result<DiarioDTO> Atualizar(DiarioDTO diarioDTO)
        {
            var communication = new Diarios();
            return communication.AtualizarAsync(diarioDTO).Result;
        }
        private static Result<DiarioDTO> Remover(int idRefeicaoGrupo, int idAlimento)
        {
            var communication = new Diarios();
            return communication.RemoverAlimentoAsync(idRefeicaoGrupo, idAlimento).Result;
        }

        private static Result<Responsavel> InserirResponsavel()
        {
            var responsavel = new Responsavel
            {
                Email = Util.CreateEmail(),
                Nome = Util.CreateString(8),
                Senha = Util.CreateString(6),
                Telefone = "1967739788"
            };

            var communication = new Responsaveis();
            return communication.InserirAsync(responsavel).Result;
        }

        private static Result<Crianca> InserirCrianca(int responsavelId)
        {
            var crianca = new Crianca
            {
                IdResponsavel = responsavelId,
                AlturaInicial = 1.5m,
                PesoInicial = 50.5m,
                Sexo = "M",
                Nome = Util.CreateString(10),
                DataNascimento = DateTime.Now.AddYears(-4)
            };

            var communication = new Criancas();
            return communication.InserirAsync(crianca).Result;
        }
    }
}
