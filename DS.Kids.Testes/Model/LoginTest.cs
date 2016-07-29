using DS.Kids.Model;
using DS.Kids.Model.Validations;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DS.Kids.Testes.Model
{
    [TestClass]
    public class LoginTest
    {
        [TestMethod]
        public void Cria_Login_Com_Informações_Validas()
        {
            var value = new Login
            {
                Email = "pedro@otavio.com.br",
                Senha = "123456"
            };
            var expected = ResultCodes.Success;
            var result = value.Validate();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Cria_Login_Com_Email_Inválido()
        {
            var value = new Login
            {
                Email = "",
                Senha = "123456"
            };
            var expected = ResultCodes.EmailObrigatorio;
            var result = value.Validate();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Cria_Login_Com_Senha_Inválidas()
        {
            var value = new Login
            {
                Email = "pedro@otavio.com.br",
                Senha = ""
            };
            var expected = ResultCodes.SenhaObrigatoria;
            var result = value.Validate();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [Description("Gerar Senha Aleatória Deve ter 6 caracteres")]
        public void Gerar_Senha_Aleatória_Deve_Ter_6_Caracteres()
        {
            var senha = Login.GerarSenhaAleatoria();
            Assert.AreEqual(senha.Length, 6);
        }
    }
}
