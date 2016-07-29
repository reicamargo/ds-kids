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
    public class SenhaTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Authorization.Singleton.KillToken();
        }

        [TestMethod]
        public void Troca_De_Senha()
        {
            var responsavel = new Responsavel
            {
                Email = Util.CreateEmail("Troca_De_Senha"),
                Nome = Util.CreateString(8),
                Senha = Util.CreateString(6),
                Telefone = "1967739788"
            };

            var expected = ResultCodes.Success;

            var responsavelResult = Criar(responsavel);
            var trocaSenhaResult = Troca(responsavelResult.Data.IdResponsavel, responsavel.Senha, Util.CreateString(6));
            Authorization.Singleton.KillToken();
            
            Assert.AreEqual(expected, responsavelResult.ResultCode);
            Assert.AreEqual(expected, trocaSenhaResult.ResultCode);
        }

        [TestMethod]
        [StatusCodeExpected(HttpStatusCode.Forbidden)]
        public void Troca_De_Senha_Deslogado()
        {
            var responsavel = new Responsavel
            {
                Email = Util.CreateEmail(),
                Nome = Util.CreateString(8),
                Senha = Util.CreateString(6),
                Telefone = "1967739788"
            };
            var responsavelResult = Criar(responsavel);
            Authorization.Singleton.KillToken();
            var trocaSenhaResult = Troca(responsavelResult.Data.IdResponsavel, responsavel.Senha, Util.CreateString(6));
        }

        [TestMethod]
        public void Esqueci_Minha_Senha()
        {
            var responsavel = new Responsavel
            {
                Email = Util.CreateEmail(),
                Nome = Util.CreateString(8),
                Senha = Util.CreateString(6),
                Telefone = "1967739788"
            };

            var expected = ResultCodes.Success;

            var responsavelResult = Criar(responsavel);
            Assert.AreEqual(expected, responsavelResult.ResultCode);

            var esqueciResult = Esqueci(responsavel.Email);
            Assert.AreEqual(expected, esqueciResult.ResultCode);
        }

        private Result Troca(int responsavelId, string senhaAtual, string novaSenha)
        {
            var trocaDeSenha = new TrocaDeSenha
            {
                IdResponsavel = responsavelId,
                SenhaAtual = senhaAtual,
                NovaSenha = novaSenha
            };

            var communication = new Senha();
            return communication.TrocaAsync(trocaDeSenha).Result;
        }

        private Result Esqueci(string email)
        {
            var communication = new Senha();
            return communication.EsqueciAsync(email).Result;
        }

        private Result<Responsavel> Criar(Responsavel responsavel)
        {
            var communication = new Responsaveis();
            return communication.InserirAsync(responsavel).Result;
        }
    }
}
