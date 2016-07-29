using DS.Kids.Model.Validations;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DS.Kids.Testes.Model.Validations
{
    [TestClass]
    public class NomeTest
    {
        [TestMethod]
        public void Nome_Deve_Ser_Obrigatório()
        {
            var expected = ResultCodes.NomeObrigatorio;
            var result = Validate.Nome("");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Nome_Deve_Conter_Menos_Do_Que_50_Carecteres()
        {
            var expected = ResultCodes.TamanhoMaximoCampoNome;
            var result = Validate.Nome(new string('x', 51));
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Nome_Deve_Conter_Menos_Do_Que_50_Carecteres_E_Ser_Obrigatório()
        {
            var expected = ResultCodes.Success;
            var result = Validate.Nome("Meu Nome");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Nome_Deve_Conter_Menos_Do_Que_50_Carecteres_Mesmo_Não_Sendo_Obrigatório()
        {
            var expected = ResultCodes.TamanhoMaximoCampoNome;
            var result = Validate.Nome(new string('x', 51), false);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Nome_Deve_Conter_Menos_Do_Que_50_Carecteres_Mesmo_Não_Sendo_Obrigatório_Mas_Contendo_Um_Valor_Válido()
        {
            var expected = ResultCodes.Success;
            var result = Validate.Nome("Meu Nome", false);
            Assert.AreEqual(expected, result);
        }
    }
}