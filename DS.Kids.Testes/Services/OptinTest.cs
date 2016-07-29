using System;

using DS.Kids.Model.Repositories;
using DS.Kids.Model.Services;
using DS.Kids.Model.Validations;
using DS.Kids.Testes.Infra;
using DS.Kids.Testes.Services.__Fakes.Repositories;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DS.Kids.Testes.Services
{
    [TestClass]
    public class OptinTest
    {
        private IResponsaveis _responsaveis;
        private ITokens _tokens;
        private Responsavel _serviceResponsavel;
        private Optin _service;

        [TestInitialize]
        public void Initialize()
        {
            var database = new Database();
            _responsaveis = new ResponsaveisFake(database);
            _tokens = new TokensFake(database);
            _service = new Optin(_responsaveis);
            _serviceResponsavel = new Responsavel(_responsaveis, _tokens);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Atualizar_Optin_Com_ResponsavelId_Negativo()
        {
            try
            {
                var x = _service.SetAsync(new Kids.Model.Optin{IdResponsavel = -10, OptinPrincipal = true}).Result;
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }

        [TestMethod]
        public void Inserir_Responsavel_Deve_Ter_Optin_True()
        {
            var responsavel = new Kids.Model.Responsavel
            {
                Email = Util.CreateEmail(),
                Nome = Util.CreateString(8),
                Senha = "123456",
                Telefone = "11987654321",
                Optin = false
            };

            var expected = ResultCodes.Success;
            var result = _serviceResponsavel.InserirAsync(responsavel).Result;
            Assert.AreEqual(expected, result.ResultCode);

            var responsavelAlterado = _responsaveis.ObterPorIdAsync(responsavel.IdResponsavel).Result;
            Assert.AreEqual(responsavelAlterado.Optin, true);
        }

        [TestMethod]
        public void Atualizar_Optin_Com_ResponsavelId_Inexistente()
        {
            var expected = ResultCodes.ResponsavelNaoEncontrado;
            var result = _service.SetAsync(new Kids.Model.Optin { IdResponsavel = int.MaxValue, OptinPrincipal = true }).Result;
            
            Assert.AreEqual(expected, result.ResultCode);
        }

        [TestMethod]
        public void Atualizar_Optin_Com_Optin_True()
        {
            var expected = ResultCodes.Success;
            var result = _service.SetAsync(new Kids.Model.Optin { IdResponsavel = 1, OptinPrincipal = true }).Result;

            Assert.AreEqual(expected, result.ResultCode);

            var responsavelAlterado = _responsaveis.ObterPorIdAsync(1).Result;
            Assert.AreEqual(responsavelAlterado.Optin, true);
        }

        [TestMethod]
        public void Atualizar_Optin_Com_Optin_False()
        {
            var expected = ResultCodes.Success;
            var result = _service.SetAsync(new Kids.Model.Optin { IdResponsavel = 1, OptinPrincipal = false }).Result;

            Assert.AreEqual(expected, result.ResultCode);

            var responsavelAlterado = _responsaveis.ObterPorIdAsync(1).Result;
            Assert.AreEqual(responsavelAlterado.Optin, false);
        }
    }
}
