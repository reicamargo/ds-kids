using DS.Kids.Model.Validations;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DS.Kids.Testes.Model.Validations
{
    [TestClass]
    public class ChaveRedeSocialTest
    {
        [TestMethod]
        public void Chave_Rede_Social_Deve_Ser_Obrigatória()
        {
            var expected = ResultCodes.ChaveRedeSocialObrigatoria;
            var result = Validate.ChaveRedeSocial("");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Chave_Rede_Social_Válida()
        {
            var expected = ResultCodes.Success;
            var result = Validate.ChaveRedeSocial("123456789");
            Assert.AreEqual(expected, result);
        }
    }
}
