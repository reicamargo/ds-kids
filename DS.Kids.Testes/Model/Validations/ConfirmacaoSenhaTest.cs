using DS.Kids.Model.Validations;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DS.Kids.Testes.Model.Validations
{
    [TestClass]
    public class ConfirmacaoSenhaTest
    {

        [TestMethod]
        public void Alteração_De_Senha_Com_Nova_Senha_Invalida()
        {
            var expected = ResultCodes.NovaSenhaInvalida;
            var result = Validate.ConfirmacaoSenha("", "senha123");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Alteração_De_Senha_Com_Confirmacao_De_Senha_Invalida()
        {
            var expected = ResultCodes.ConfirmacaoSenhaInvalida;
            var result = Validate.ConfirmacaoSenha("senha123", "");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Alteração_De_Senha_Com_Nova_Senha_Igual_A_Senha_Atual()
        {
            var expected = ResultCodes.NovaSenhaIgualSenhaAtual;
            var result = Validate.ConfirmacaoSenha("senha123", "senha123");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Alteração_De_Senha_Com_Informações_Válidas()
        {
            var expected = ResultCodes.Success;
            var result = Validate.ConfirmacaoSenha("senha123", "123senha");
            Assert.AreEqual(expected, result);
        }
    }
}
