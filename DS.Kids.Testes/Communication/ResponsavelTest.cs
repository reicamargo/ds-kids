using System.Net;

using DS.Kids.Model;
using DS.Kids.Model.Communication;
using DS.Kids.Model.Validations;
using DS.Kids.Testes.Infra;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Authorization = DS.Kids.Model.Communication.Authorization;
using Login = DS.Kids.Model.Communication.Login;

namespace DS.Kids.Testes.Communication
{
    [TestClass]
    public class ResponsavelTest
    {
        private readonly string _senha = Util.CreateString(6);

        [TestInitialize]
        public void Initialize()
        {
            Authorization.Singleton.KillToken();
        }

        [TestMethod]
        public void Inserir_Responsavel()
        {
            var responsavel = new Responsavel
            {
                Email = Util.CreateEmail(),
                Nome = Util.CreateString(8),
                Senha = _senha,
                Telefone = "1967739788"
            };

            var expected = ResultCodes.Success;
            var result = Criar(responsavel);
            Assert.AreEqual(expected, result.ResultCode);

            var communication = new Login();
            var logoff = communication.LogoffAsync(result.Data.IdResponsavel).Result;
            var login = communication.LogarAsync(new Kids.Model.Login { Email = responsavel.Email, Senha = responsavel.Senha }).Result;
            Assert.IsNotNull(login.Data);
            Assert.IsNotNull(login.Data.Token);
        }

        [TestMethod]
        public void Atualizar_Responsavel()
        {
            var responsavel = new Responsavel
            {
                Email = Util.CreateEmail(),
                Nome = Util.CreateString(8),
                Senha = _senha,
                Telefone = "1967739788"
            };

            var expected = ResultCodes.Success;
            var result = Criar(responsavel);
            result.Data.Nome = Util.CreateString(8);
            result.Data.Senha = _senha;
            var resultAtualizar = Atualizar(result.Data);
            Authorization.Singleton.KillToken();
            Assert.AreEqual(expected, result.ResultCode);
            Assert.AreEqual(expected, resultAtualizar.ResultCode);
        }

        [TestMethod]
        [StatusCodeExpected(HttpStatusCode.Forbidden)]
        public void Atualizar_Responsavel_Deslogado()
        {
            var responsavel = new Responsavel
            {
                Email = Util.CreateEmail(),
                Nome = Util.CreateString(8),
                Senha = _senha,
                Telefone = "1967739788",
                IdResponsavel = 1
            };

            var resultAtualizar = Atualizar(responsavel);
        }

        private Result<Responsavel> Criar(Responsavel responsavel)
        {
            var communication = new Responsaveis();
            return communication.InserirAsync(responsavel).Result;
        }

        private Result Atualizar(Responsavel responsavel)
        {
            var communication = new Responsaveis();
            return communication.AtualizarAsync(responsavel).Result;
        }
    }
}
