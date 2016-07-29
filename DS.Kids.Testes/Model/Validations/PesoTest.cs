using DS.Kids.Model.Validations;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DS.Kids.Testes.Model.Validations
{
    [TestClass]
    public class PesoTest
    {
        [TestMethod]
        public void A_Criança_Não_Pode_Ter_Peso_Menor_Do_Que_5()
        {
            var expected = ResultCodes.PesoInvalido;
            var result = Validate.Peso(4);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void A_Criança_Não_Pode_Ter_Peso_Maior_Do_Que_120()
        {
            var expected = ResultCodes.PesoInvalido;
            var result = Validate.Peso(121);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void A_Criança_Não_Pode_Ter_Peso_Menor_Do_Que_5_Ou_Maior_Do_Que_120()
        {
            var expected = ResultCodes.Success;
            var result = Validate.Peso(30);
            Assert.AreEqual(expected, result);
        }
    }
}
