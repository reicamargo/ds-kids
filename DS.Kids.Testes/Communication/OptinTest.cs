using System.Net;

using DS.Kids.Model;
using DS.Kids.Model.Communication;
using DS.Kids.Model.Validations;
using DS.Kids.Testes.Infra;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Authorization = DS.Kids.Model.Communication.Authorization;
using Login = DS.Kids.Model.Communication.Login;
using Optin = DS.Kids.Model.Communication.Optin;

namespace DS.Kids.Testes.Communication
{
    [TestClass]
    public class OptinTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Authorization.Singleton.KillToken();
        }

        [TestMethod]
        public void Atualizar_Optin_Com_Optin_False()
        {
            var expected = ResultCodes.Success;

            var responsavel = new Responsavel
            {
                Email = Util.CreateEmail("Troca_De_Senha"),
                Nome = Util.CreateString(8),
                Senha = Util.CreateString(6),
                Telefone = "1967739788",
                Optin = false
            };

            var responsavelResult = Criar(responsavel);
            Assert.AreEqual(expected, responsavelResult.ResultCode);
            Assert.IsTrue(responsavelResult.Data.Optin);

            var communication = new Optin();
            var result = communication.SetAsync(new Kids.Model.Optin { IdResponsavel = responsavelResult.Data.IdResponsavel, OptinPrincipal = false }).Result;

            var communicationLogin = new Login();
            var logoff = communicationLogin.LogoffAsync(responsavelResult.Data.IdResponsavel).Result;
            var login = communicationLogin.LogarAsync(new Kids.Model.Login { Email = responsavel.Email, Senha = responsavel.Senha }).Result;
            Assert.IsFalse(login.Data.Optin);

            Authorization.Singleton.KillToken();
            Assert.AreEqual(expected, result.ResultCode);
        }

        private static Result<Responsavel> Criar(Responsavel responsavel)
        {
            var communication = new Responsaveis();
            return communication.InserirAsync(responsavel).Result;
        }

        [TestMethod]
        [StatusCodeExpected(HttpStatusCode.Forbidden)]
        public void Atualizar_Optin_Id_Deslogado()
        {
            Authorization.Singleton.KillToken();
            var communication = new Optin();
            var result = communication.SetAsync(new Kids.Model.Optin{IdResponsavel = 1, OptinPrincipal = true}).Result;
        }
    }
}
