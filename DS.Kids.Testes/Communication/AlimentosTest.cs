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
    public class AlimentosTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Authorization.Singleton.KillToken();
        }

        [TestMethod]
        public void Obter_Alimentos()
       { 
            var expected = ResultCodes.Success;

            var responsavelResult = InserirResponsavel();
            Assert.AreEqual(expected, responsavelResult.ResultCode);

            var criancaResult = InserirCrianca(responsavelResult.Data.IdResponsavel);
            Assert.AreEqual(expected, criancaResult.ResultCode);

            for (int i = 24; i < 11 * 12; i++)
            {
                var result = Obter(i, (int)TipoGrupoRefeicao.AcucaresEDoces);

                Assert.AreEqual(expected, result.ResultCode);
                Assert.IsNotNull(result.Data);
                Assert.IsTrue(result.Data.Any());
            }

            Authorization.Singleton.KillToken();

            Authorization.Singleton.KillToken();
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void Obter_Alimentos_Sem_Autorizacao()
        {
            var expected = ResultCodes.AcessoNegado;
            var result = Obter(36, (int)TipoGrupoRefeicao.AcucaresEDoces);

            Assert.AreEqual(expected, result.ResultCode);
        }

        private static Result<IEnumerable<Alimento>> Obter(int mesesDeIdade, int idGrupo)
        {
            var communication = new Alimentos();
            return communication.ObterPorGrupoAlimentar(mesesDeIdade, idGrupo).Result;
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
