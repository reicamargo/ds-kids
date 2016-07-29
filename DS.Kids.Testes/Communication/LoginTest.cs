using System.Net;

using DS.Kids.Model;
using DS.Kids.Model.Communication;
using DS.Kids.Model.Validations;
using DS.Kids.Testes.Infra;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Authorization = DS.Kids.Model.Communication.Authorization;
using Login = DS.Kids.Model.Login;

namespace DS.Kids.Testes.Communication
{
    [TestClass]
    public class LoginTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Authorization.Singleton.KillToken();
        }

        [TestMethod]
        public void Criar_Conta()
        {
            var responsavel = new Responsavel
            {
                Email = Util.CreateEmail(),
                Nome = Util.CreateString(8),
                Senha = Util.CreateString(6),
                Telefone = "1967739788"
            };

            var expected = ResultCodes.Success;
            var result = Criar(responsavel);
            Assert.AreEqual(expected, result.ResultCode);
        }

        [TestMethod]
        public void Efetuar_Login()
        {
            var responsavel = new Responsavel
            {
                Email = Util.CreateEmail(),
                Nome = Util.CreateString(8),
                Senha = Util.CreateString(6),
                Telefone = "1967739788"
            };

            var expected = ResultCodes.Success;
            var result = Criar(responsavel);
            Assert.AreEqual(expected, result.ResultCode);

            var login = new Login
            {
                Email = responsavel.Email,
                Senha = responsavel.Senha
            };

            var communication = new Kids.Model.Communication.Login();
            result = communication.LogarAsync(login).Result;

            Assert.AreEqual(expected, result.ResultCode);
        }

        [TestMethod]
        public void Efetuar_Login_Dados_Invalidos()
        {
            var login = new Login
            {
                Email = Util.CreateEmail(),
                Senha = Util.CreateString(6)
            };
            var expected = ResultCodes.LoginOuSenhaInvalidos;
            var communication = new Kids.Model.Communication.Login();
            var result = communication.LogarAsync(login).Result;

            Assert.AreEqual(expected, result.ResultCode);
        }

        [TestMethod]
        public void Efetuar_Login_Social()
        {
            var loginSocial = new LoginSocial
            {
                Email = Util.CreateEmail(),
                Nome = Util.CreateString(8),
                Chave = Util.CreateString(10),
            };

            var expected = ResultCodes.Success;
            var result = LoginSocial(loginSocial);
            Assert.AreEqual(expected, result.ResultCode);
        }

        [TestMethod]
        public void Efetuar_Logoff()
        {
            var responsavel = new Responsavel
            {
                Email = Util.CreateEmail("Efetuar_Logoff"),
                Nome = Util.CreateString(8),
                Senha = Util.CreateString(6),
                Telefone = "1967739788"
            };

            var expected = ResultCodes.Success;
            var result = Criar(responsavel);
            Assert.AreEqual(expected, result.ResultCode);

            var resultLogoff = Logoff(result.Data.IdResponsavel);
            Authorization.Singleton.KillToken();            
            Assert.AreEqual(expected, resultLogoff.ResultCode);
        }

        [TestMethod]
        [StatusCodeExpected(HttpStatusCode.Forbidden)]
        public void Efetuar_Logoff_Deslogado()
        {
            Authorization.Singleton.KillToken();
            var resultLogoff = Logoff(1);
        }

        private Result<Responsavel> Criar(Responsavel responsavel)
        {
            var communication = new Responsaveis();
            return communication.InserirAsync(responsavel).Result;
        }

        private Result Logoff(int responsavelId)
        {
            var communication = new Kids.Model.Communication.Login();
            return communication.LogoffAsync(responsavelId).Result;
        }

        private Result<Responsavel> LoginSocial(LoginSocial loginSocial)
        {
            var communication = new Kids.Model.Communication.Login();
            return communication.LogarRedeSocialAsync(loginSocial).Result;
        }

        private Result<Responsavel> Logar(string email, string senha)
        {
            var communication = new Kids.Model.Communication.Login();
            return communication.LogarAsync(new Login { Email = email, Senha = senha }).Result;
        }
    }
}
