using System;

using DS.Kids.Model.Validations;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DS.Kids.Testes.Model.Validations
{
    [TestClass]
    public class DataNascimentoCriancaTest
    {
        [TestMethod]
        public void A_Criança_Não_Pode_Ter_Menos_Do_Que_2_Anos()
        {
            var value = DateTime.Now.AddYears(-1);
            var expected = ResultCodes.DataNascimentoInvalidaCrianca;
            var result = Validate.DataNascimentoCrianca(value);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void A_Criança_Não_Pode_Ter_Mais_Do_Que_12_Anos()
        {
            var value = DateTime.Now.AddYears(-13);
            var expected = ResultCodes.DataNascimentoInvalidaCrianca;
            var result = Validate.DataNascimentoCrianca(value);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void A_Criança_Não_Pode_Ter_Menos_Do_Que_2_Anos_Ou_Mais_Do_Que_12_Anos()
        {
            var value = DateTime.Now.AddYears(-10);
            var expected = ResultCodes.Success;
            var result = Validate.DataNascimentoCrianca(value);
            Assert.AreEqual(expected, result);
        }
    }
}
