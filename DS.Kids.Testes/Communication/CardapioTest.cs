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
    public class CardapioTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Authorization.Singleton.KillToken();
        }

        [TestMethod]
        public void Obter_Cardapio()
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

            var result = Obter();
            Authorization.Singleton.KillToken();

            Assert.AreEqual(expected, result.ResultCode);
        }

        [TestMethod]
        [StatusCodeExpected(HttpStatusCode.Forbidden)]
        public void Obter_Cardapio_Deslogado()
        {
            var result = Obter();
        }

        [TestMethod]
        public void Substituir_Cardapio()
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

            var result = Substituir();

            Authorization.Singleton.KillToken();
            Assert.AreEqual(expected, result.ResultCode);
        }

        [TestMethod]
        [StatusCodeExpected(HttpStatusCode.Forbidden)]
        public void Substituir_Cardapio_Deslogado()
        {
            var result = Substituir();
        }

        private Result<Cardapio> Obter()
        {
            var communication = new Cardapios();
            return communication.ObterAsync(48).Result;
        }

        private Result<Refeicao> Substituir()
        {
            var communication = new Cardapios();
            return communication.SubstituirRefeicaoAsync(48, TipoRefeicao.LancheDaTarde).Result;
        }

        private Result<Responsavel> Criar(Responsavel responsavel)
        {
            var communication = new Responsaveis();
            return communication.InserirAsync(responsavel).Result;
        }
    }
}
