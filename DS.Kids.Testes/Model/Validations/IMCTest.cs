using DS.Kids.Model.Validations;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DS.Kids.Testes.Model.Validations
{
    [TestClass]
    public class IMCTest
    {
        [TestMethod]
        public void A_Criança_Não_Pode_Ter_IMC_Menor_Do_Que_3_2()
        {
            var expected = ResultCodes.ImcInvalido;
            var result = Validate.IMC(3.1m);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void A_Criança_Não_Pode_Ter_IMC_Maior_Do_Que_30()
        {
            var expected = ResultCodes.ImcInvalido;
            var result = Validate.IMC(31);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void A_Criança_Não_Pode_Ter_IMC_Menor_Do_Que_3_2_Ou_Maior_Do_Que_30()
        {
            var expected = ResultCodes.Success;
            var result = Validate.IMC(20);
            Assert.AreEqual(expected, result);
        }
    }
}
