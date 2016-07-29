using DS.Kids.Model;
using DS.Kids.Model.Validations;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DS.Kids.Testes.Model
{
    [TestClass]
    public class LoginSocialTest
    {
        [TestMethod]
        public void Cria_Login_Social_Valido()
        {
            var value = new LoginSocial
            {
                IdResponsavel = 1,
                RedeSocial = RedesSociais.Facebook,
                Nome = "Pedro Otávio",
                Email = "pedro@otavio.com.br",
                Chave = "123456789"
            };
            var expected = ResultCodes.Success;
            var result = value.Validate();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Cria_Login_Social_Com_Nome_Inválido()
        {
            var value = new LoginSocial
            {
                IdResponsavel = 1,
                RedeSocial = RedesSociais.Facebook,
                Nome = "",
                Email = "pedro@otavio.com.br",
                Chave = "123456789"
            };
            var expected = ResultCodes.NomeObrigatorio;
            var result = value.Validate();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Cria_Login_Social_Com_Email_Inválido()
        {
            var value = new LoginSocial
            {
                IdResponsavel = 1,
                RedeSocial = RedesSociais.Facebook,
                Nome = "Pedro Otávio",
                Email = "",
                Chave = "123456789"
            };
            var expected = ResultCodes.EmailObrigatorio;
            var result = value.Validate();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Cria_Login_Social_Com_Chave_Rede_Social_Inválido()
        {
            var value = new LoginSocial
            {
                IdResponsavel = 1,
                RedeSocial = RedesSociais.Facebook,
                Nome = "Pedro Otávio",
                Email = "pedro@otavio.com.br",
                Chave = ""
            };
            var expected = ResultCodes.ChaveRedeSocialObrigatoria;
            var result = value.Validate();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Cria_Responsavel()
        {
            var value = new LoginSocial
            {
                IdResponsavel = 1,
                RedeSocial = RedesSociais.Facebook,
                Nome = "Pedro Otávio",
                Email = "pedro@otavio.com.br",
                Chave = "123456"
            };
            var result = value.CriarResponsavel();
            Assert.IsNotNull(result);
        }
    }
}