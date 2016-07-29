using System;

using DS.Kids.Model.Validations;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DS.Kids.Testes.Model.Validations
{
    [TestClass]
    public class TokenRecuperacaoSenhaTest
    {
        [TestMethod]
        public void Token_Recuperacao_Senha_Deve_Ser_Obrigatório()
        {
            var expected = ResultCodes.TokenRecuperacaoSenhaInvalido;
            var result = Validate.TokenRecuperacaoSenha(string.Empty);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Token_Recuperacao_Senha_Válido()
        {
            var expected = ResultCodes.Success;
            var result = Validate.TokenRecuperacaoSenha(Guid.NewGuid().ToString());
            Assert.AreEqual(expected, result);
        }
    }
}
