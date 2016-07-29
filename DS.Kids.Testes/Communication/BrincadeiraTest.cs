using System.Net;

using DS.Kids.Model;
using DS.Kids.Model.Communication;
using DS.Kids.Model.Validations;
using DS.Kids.Testes.Infra;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Authorization = DS.Kids.Model.Communication.Authorization;

namespace DS.Kids.Testes.Communication
{
    [TestClass]
    public class BrincadeiraTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Authorization.Singleton.KillToken();
        }

        [TestMethod]
        public void Obter_Brincadeira()
        {
            var expected = ResultCodes.Success;

            var responsavel = new Responsavel
            {
                Email = Util.CreateEmail("Troca_De_Senha"),
                Nome = Util.CreateString(8),
                Senha = Util.CreateString(6),
                Telefone = "1967739788"
            };
            var responsavelResult = Criar(responsavel);
            Assert.AreEqual(expected, responsavelResult.ResultCode);

            var communication = new Brincadeiras();
            var result = communication.ObterPorIdAsync(1).Result;
            Authorization.Singleton.KillToken();
            Assert.AreEqual(expected, result.ResultCode);
        }

        [TestMethod]
        [StatusCodeExpected(HttpStatusCode.Forbidden)]
        public void Obter_Brincadeira_Deslogado()
        {
            var communication = new Brincadeiras();
            var result = communication.ObterPorIdAsync(1).Result;
        }

        [TestMethod]
        public void Obter_Ultimas_Brincadeiras()
        {
            var expected = ResultCodes.Success;

            var responsavel = new Responsavel
            {
                Email = Util.CreateEmail("Troca_De_Senha"),
                Nome = Util.CreateString(8),
                Senha = Util.CreateString(6),
                Telefone = "1967739788"
            };
            var responsavelResult = Criar(responsavel);
            Assert.AreEqual(expected, responsavelResult.ResultCode);

            var communication = new Brincadeiras();
            var result = communication.ObterUltimasBrincadeirasAsync(3, 1).Result;
            Authorization.Singleton.KillToken();
            Assert.AreEqual(expected, result.ResultCode);
        }

        [TestMethod]
        [StatusCodeExpected(HttpStatusCode.Forbidden)]
        public void Obter_Ultimas_Brincadeiras_Deslogado()
        {
            Authorization.Singleton.KillToken();
            var communication = new Brincadeiras();
            var result = communication.ObterUltimasBrincadeirasAsync(3, 1).Result;
        }

        private Result<Responsavel> Criar(Responsavel responsavel)
        {
            var communication = new Responsaveis();
            return communication.InserirAsync(responsavel).Result;
        }
    }
}
