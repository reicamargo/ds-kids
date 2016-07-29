using System;

using DS.Kids.Model;
using DS.Kids.Model.Validations;
using DS.Kids.Testes.Infra;
using DS.Kids.Testes.Services.__Fakes.Repositories;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Login = DS.Kids.Model.Services.Login;
using Responsavel = DS.Kids.Model.Services.Responsavel;

namespace DS.Kids.Testes.Services
{
    [TestClass]
    public class LoginTest
    {
        private Login _service;

        private ResponsaveisFake _responsaveis;

        private TokensFake _tokens;

        private LoginsSociaisFake _loginsSociais;

        private Responsavel _serviceResponsavel;

        [TestInitialize]
        public void Initialize()
        {
            var database = new Database();
            _responsaveis = new ResponsaveisFake(database);
            _tokens = new TokensFake(database);
            _loginsSociais = new LoginsSociaisFake(database);
            _service = new Login(_responsaveis, _tokens, _loginsSociais);
            _serviceResponsavel = new Responsavel(_responsaveis, _tokens);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void Logar_Com_Informações_Do_Logins_Nula()
        {
            var x = _service.LogarAsync(null).Result;
        }

        [TestMethod]
        public void Logar_Com_Responsavel_Inexistente()
        {
            var value = new Kids.Model.Login
                            {
                Email = "uuuuuuu@aaaaaa.com.br",
                Senha = "123456"
            };
            var expected = ResultCodes.LoginOuSenhaInvalidos;
            var result = _service.LogarAsync(value).Result;
            Assert.AreEqual(expected, result.ResultCode);
        }

        [TestMethod]
        public void Logar_Com_Senha_Invalida()
        {
            var value = new Kids.Model.Login
                            {
                Email = "jose@email.com.br",
                Senha = "456789"
            };
            var expected = ResultCodes.LoginOuSenhaInvalidos;
            var result = _service.LogarAsync(value).Result;
            Assert.AreEqual(expected, result.ResultCode);
        }

        [TestMethod]
        public void Efetuar_Login_Com_Informacoes_Validas()
        {


            var value = new Kids.Model.Login
                            {
                Email = "jose@email.com.br",
                Senha = "123456"
            };
            var expected = ResultCodes.Success;
            var result = _service.LogarAsync(value).Result;
            Assert.AreEqual(expected, result.ResultCode);
            Assert.IsNotNull(result.Data.Token);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void Logar_Com_Informações_Do_Logins_Social_Nula()
        {
            var x = _service.LogarRedeSocialAsync(null).Result;
        }

        [TestMethod]
        public void Logar_Com_Email_Nao_Cadastrado_Deve_Criar_Um_Novo_Registro()
        {
            var value = new LoginSocial
                            {
                Email = Util.CreateEmail(),
                Nome = "Nome Login Social",
                Chave = "123456",
                RedeSocial = RedesSociais.Facebook
            };

            var result = _service.LogarRedeSocialAsync(value).Result;
            Assert.IsNotNull(result.Data.Token);

            var responsavel = _responsaveis.ObterPorEmailAsync(value.Email);

            var loginSocial = _loginsSociais.ObterPorResponsavelIdRedeSocialAsync(responsavel.Id, value.RedeSocial);

            Assert.IsNotNull(responsavel);
            Assert.IsNotNull(loginSocial);            
        }

        [TestMethod]
        public void Logar_Com_Email_Ja_Existente_Sem_Login_Social_Deve_Criar_Apenas_Um_Login_Social()
        {
            /*Cria um Responsável*/
            
            var novoResponsavel = new Kids.Model.Responsavel
            {
                IdResponsavel = 5,
                Nome = "Roberval",
                Senha = "123456",
                Telefone = "11987654325",
                Email = Util.CreateEmail()
            };

            var x = _serviceResponsavel.InserirAsync(novoResponsavel).Result;
            /*Cria um Responsável*/

            var value = new LoginSocial
                            {
                Email = novoResponsavel.Email,
                Nome = novoResponsavel.Nome,
                Chave = "123456",
                RedeSocial = RedesSociais.Facebook
            };

            var result = _service.LogarRedeSocialAsync(value).Result;
            Assert.IsNotNull(result.Data.Token);

            var responsavel = _responsaveis.ObterPorEmailAsync(value.Email);
            
            var loginSocial = _loginsSociais.ObterPorResponsavelIdRedeSocialAsync(responsavel.Id, value.RedeSocial);

            Assert.IsNotNull(responsavel);
            Assert.IsNotNull(loginSocial);
        }

        [TestMethod]
        public void Logar_Com_Email_Ja_Existente_E_Com_Login_Social_Ja_Existente_Não_Deve_Criar_Nenhum_Registro()
        {
            var value = new LoginSocial
                            {
                Email = "jose@email.com.br",
                Nome = "José",
                Chave = "123 ",
                RedeSocial = RedesSociais.Facebook
            };

            var result = _service.LogarRedeSocialAsync(value).Result;
            Assert.IsNotNull(result.Data.Token);

            var responsavel = _responsaveis.ObterPorEmailAsync(value.Email);

            var loginSocial = _loginsSociais.ObterPorResponsavelIdRedeSocialAsync(responsavel.Id, value.RedeSocial);

            Assert.IsNotNull(responsavel);
            Assert.IsNotNull(loginSocial);
        }

        [TestMethod]
        public void Efetuar_Logoff_Deve_Excluir_Informações_De_Token()
        {
            var responsavel = _responsaveis.ObterPorIdAsync(1).Result;

            var expected = ResultCodes.Success;
            var result = _service.LogoffAsync(responsavel.IdResponsavel).Result;

            var token = _tokens.ObterPorResponsavelIdAsync(1).Result;

            Assert.AreEqual(expected, result.ResultCode);
            Assert.IsNull(token);
        }
    }
}
