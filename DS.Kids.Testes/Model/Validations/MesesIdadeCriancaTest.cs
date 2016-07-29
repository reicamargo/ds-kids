using DS.Kids.Model.Validations;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DS.Kids.Testes.Model.Validations
{
    [TestClass]
    public class MesesIdadeCriancaTest
    {
        [TestMethod]
        public void A_Criança_Não_Pode_Ter_Menos_Do_Que_24_Meses_De_Idade()
        {
            var expected = ResultCodes.DataNascimentoInvalidaCrianca;
            var result = Validate.MesesIdadeCrianca(23);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void A_Criança_Não_Pode_Ter_Mais_Do_Que_144_Meses_De_Idade()
        {
            var expected = ResultCodes.DataNascimentoInvalidaCrianca;
            var result = Validate.MesesIdadeCrianca(145);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void A_Criança_Não_Pode_Ter_Menos_Do_Que_2_Anos_Ou_Mais_Do_Que_12_Anos()
        {
            var expected = ResultCodes.Success;
            var result = Validate.MesesIdadeCrianca(120);
            Assert.AreEqual(expected, result);
        }
    }
}
