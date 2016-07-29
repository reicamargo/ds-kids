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
    public class ResponsavelTest
    {
        private IResponsaveis _responsaveis;
        private ITokens _tokens;
        private Responsavel _service;

        [TestInitialize]
        public void Initialize()
        {
            var database = new Database();
            _responsaveis = new ResponsaveisFake(database);
            _tokens = new TokensFake(database);
            _service = new Responsavel(_responsaveis, _tokens);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Inserir_Responsavel_Com_Entidade_Nula()
        {
            try
            {
                var x = _service.InserirAsync(null).Result;
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Atualizar_Responsavel_Com_Entidade_Nula()
        {
            try
            {
                var x = _service.AtualizarAsync(null).Result;
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }

        [TestMethod]
        public void Inserir_Responsavel_Com_Informacoes_Invalidas()
        {
            var responsavel = new Kids.Model.Responsavel();

            var expected = ResultCodes.Success;
            var result = _service.InserirAsync(responsavel).Result;
            Assert.AreNotEqual(expected, result.ResultCode);
        }

        [TestMethod]
        public void Atualizar_Responsavel_Com_Informacoes_Invalidas()
        {
            var responsavel = new Kids.Model.Responsavel();

            var expected = ResultCodes.Success;
            var result = _service.AtualizarAsync(responsavel).Result;
            Assert.AreNotEqual(expected, result.ResultCode);
        }

        [TestMethod]
        public void Inserir_Responsavel_Com_Email_Duplicado()
        {
            var responsavel = new Kids.Model.Responsavel
            {
                Nome = "Meu Nome",
                Email = "jose@email.com.br",
                Senha = "123456",
                Telefone = "11987654321"
            };

            var expected = ResultCodes.EmailResponsavelJaCadastradoNoSistema;
            var result = _service.InserirAsync(responsavel).Result;
            Assert.AreEqual(expected, result.ResultCode);
        }

        [TestMethod]
        public void Inserir_Responsavel_Com_Informacoes_Validas()
        {
            var responsavel = new Kids.Model.Responsavel
            {
                Email = Util.CreateEmail(),
                Nome = Util.CreateString(8),
                Senha = "123456",
                Telefone = "11987654321"
            };

            var expected = ResultCodes.Success;
            var result = _service.InserirAsync(responsavel).Result;
            Assert.AreEqual(expected, result.ResultCode);
        }

        [TestMethod]
        public void Atualizar_Responsavel_Com_Informacoes_Validas()
        {
            var responsavel = new Kids.Model.Responsavel
            {
                Email = Util.CreateEmail(),
                Nome = Util.CreateString(8),
                Senha = "123456",
                Telefone = "11987654321"
            };

            var expected = ResultCodes.Success;
            var resultInserir = _service.InserirAsync(responsavel).Result;
            Assert.AreEqual(expected, resultInserir.ResultCode);

            responsavel.Nome = string.Format("alterado - {0}", responsavel.Nome);
            responsavel.Senha = "123456";

            var resultAtualizar = _service.AtualizarAsync(responsavel).Result;
            Assert.AreEqual(expected, resultAtualizar.ResultCode);

            var responsavelAlterado = _responsaveis.ObterPorIdAsync(responsavel.IdResponsavel).Result;
            Assert.AreEqual(responsavelAlterado.Nome, responsavel.Nome);
        }
    }
}
