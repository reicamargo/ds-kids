using DS.Kids.Model;
using DS.Kids.Model.Services;
using DS.Kids.Model.Validations;
using DS.Kids.Testes.Services.__Fakes.Events;
using DS.Kids.Testes.Services.__Fakes.Repositories;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DS.Kids.Testes.Services
{
    [TestClass]
    public class SenhaTest
    {
        private ResponsaveisFake _repositories;
        private Senha _service;
        [TestInitialize]
        public void Initialize()
        {
            var database = new Database();
            _repositories = new ResponsaveisFake(database);
            var @event = new SenhaFake();
            _service = new Senha(@event, _repositories);
        }

        [TestMethod]
        public void Esqueci_Senha_Com_Email_Inválido()
        {
            var expected = ResultCodes.EmailInvalido;
            var result = _service.EsqueciAsync("dasdsdadsad").Result;

            Assert.AreEqual(expected, result.ResultCode);
        }

        [TestMethod]
        public void Esqueci_Senha_Com_Responsavel_Nao_Encontrado()
        {
            var expected = ResultCodes.ResponsavelNaoEncontrado;
            var result = _service.EsqueciAsync("responsavel@naoencontrado.com.br").Result;

            Assert.AreEqual(expected, result.ResultCode);
        }

        [TestMethod]
        public void Esqueci_Senha_Deve_Gerar_Um_Token_De_Recuperacao_Senha()
        {
            var email = "jose@email.com.br";

            var expected = ResultCodes.Success;
            var result = _service.EsqueciAsync(email).Result;
            Assert.AreEqual(expected, result.ResultCode);

            var responsavel = _repositories.ObterPorEmailAsync(email).Result;
            Assert.IsNotNull(responsavel.TokenRecuperacaoSenha);
        }

        [TestMethod]
        public void Troca_De_Senha_Com_Dados_Invalidos()
        {
            var trocaSenha = new TrocaDeSenha { IdResponsavel = 1, NovaSenha = "sdas", SenhaAtual = "" };
            var expected = ResultCodes.Success;
            var result = _service.TrocaAsync(trocaSenha);
            Assert.AreNotEqual(expected, result.Result.ResultCode);
        }


        [TestMethod]
        public void Troca_De_Senha_Con_Responsável_Inexistente()
        {
            var trocaSenha = new TrocaDeSenha { IdResponsavel = 0, NovaSenha = "123456", SenhaAtual = "456789" };
            var expected = ResultCodes.ResponsavelNaoEncontrado;
            var result = _service.TrocaAsync(trocaSenha);
            Assert.AreEqual(expected, result.Result.ResultCode);
        }

        [TestMethod]
        public void Troca_De_Senha_Com_Senha_Atual_Inválida()
        {
            var trocaSenha = new TrocaDeSenha { IdResponsavel = 1, NovaSenha = "xpto1523", SenhaAtual = "xpto123" };
            var expected = ResultCodes.LoginOuSenhaInvalidos;
            var result = _service.TrocaAsync(trocaSenha);
            Assert.AreEqual(expected, result.Result.ResultCode);
        }

        [TestMethod]
        public void Troca_De_Senha_Deve_Excluir_Token_Recuperacao_Senha()
        {
            var responsavel = _repositories.ObterPorIdAsync(1).Result;
            responsavel.Senha = "ba3253876aed6bc22d4a6ff53d8406c6ad864195ed144ab5c87621b6c233b548baeae6956df346ec8c17f5ea10f35ee3cbc514797ed7ddd3145464e2a0bab413";
            responsavel.TokenRecuperacaoSenha = "12312312313";

            var trocaSenha = new TrocaDeSenha { IdResponsavel = responsavel.IdResponsavel, NovaSenha = "xpto1523", SenhaAtual = "123456" };
            
            var expected = ResultCodes.Success;
            var result = _service.TrocaAsync(trocaSenha);
            Assert.AreEqual(expected, result.Result.ResultCode);
            Assert.IsTrue(string.IsNullOrEmpty(responsavel.TokenRecuperacaoSenha));
        }

        [TestMethod]
        public void Troca_De_Senha_Com_Dados_Validos()
        {
            var responsavel = _repositories.ObterPorIdAsync(1).Result;
            responsavel.Senha = "ba3253876aed6bc22d4a6ff53d8406c6ad864195ed144ab5c87621b6c233b548baeae6956df346ec8c17f5ea10f35ee3cbc514797ed7ddd3145464e2a0bab413";
            var trocaSenha = new TrocaDeSenha { IdResponsavel = responsavel.IdResponsavel, NovaSenha = "456789", SenhaAtual = "123456" };

            var expected = ResultCodes.Success;
            var result = _service.TrocaAsync(trocaSenha);
            Assert.AreEqual(expected, result.Result.ResultCode);
            Assert.AreEqual(responsavel.Senha, "514cced049c27692ff86d29c7f939a470fa403d5189d4245309015b95a5a17617d7faa4477a8e45faa41c42c8c4edb698b01c8965d0b68e67d4902255b6d8ece");
        }
    }
}
