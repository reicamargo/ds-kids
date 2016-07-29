using DS.Kids.Model.Validations;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DS.Kids.Testes.Model.Validations
{
    [TestClass]
    public class AlturaTest
    {
        [TestMethod]
        public void A_Criança_Não_Pode_Ter_Altura_Menor_Do_Que_80()
        {
            var expected = ResultCodes.AlturaInvalida;
            var result = Validate.Altura(0.79m);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void A_Criança_Não_Pode_Ter_Altura_Maior_Do_Que_200()
        {
            var expected = ResultCodes.AlturaInvalida;
            var result = Validate.Altura(2.01m);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void A_Criança_Não_Pode_Ter_Altura_Menor_Do_Que_80_Ou_Maior_Do_Que_200()
        {
            var expected = ResultCodes.Success;
            var result = Validate.Altura(1.50m);
            Assert.AreEqual(expected, result);
        }
    }
}
