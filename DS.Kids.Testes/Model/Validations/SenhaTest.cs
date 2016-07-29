using DS.Kids.Model.Validations;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DS.Kids.Testes.Model.Validations
{
    [TestClass]
    public class SenhaTest
    {
        [TestMethod]
        public void Senha_Deve_Ser_Obrigatória()
        {
            var expected = ResultCodes.SenhaObrigatoria;
            var result = Validate.Senha("");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Senha_Deve_Conter_Menos_Do_Que_20_Carecteres()
        {
            var expected = ResultCodes.TamanhoMaximoCampoSenha;
            var result = Validate.Senha(new string('x', 21));
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Senha_Deve_Conter_Mais_Do_Que_6_Carecteres()
        {
            var expected = ResultCodes.TamanhoMinimoCampoSenha;
            var result = Validate.Senha("M");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Senha_Deve_Conter_Menos_Do_Que_20_Carecteres_E_Ser_Obrigatória()
        {
            var expected = ResultCodes.Success;
            var result = Validate.Senha("Minha Senha");
            Assert.AreEqual(expected, result);
        }
    }
}
