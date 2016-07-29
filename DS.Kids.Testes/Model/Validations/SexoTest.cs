using DS.Kids.Model.Validations;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DS.Kids.Testes.Model.Validations
{
    [TestClass]
    public class SexoTest
    {
        [TestMethod]
        public void Sexo_Deve_Ser_Obrigatório()
        {
            var expected = ResultCodes.SexoObrigatorio;
            var result = Validate.Sexo("");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Sexo_Deve_Conter_M_Ou_F()
        {
            var expected = ResultCodes.SexoInvalido;
            var result = Validate.Sexo("X");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Sexo_Deve_Conter_M_Ou_F_Mesmo_Não_Sendo_Obrigatório()
        {
            var expected = ResultCodes.SexoInvalido;
            var result = Validate.Sexo("X", false);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Sexo_Feminino_Deve_Ser_M()
        {
            var expected = ResultCodes.Success;
            var result = Validate.Sexo("M");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Sexo_Masculino_Deve_Ser_M_Mesmo_Não_Sendo_Obrigatório()
        {
            var expected = ResultCodes.Success;
            var result = Validate.Sexo("M", false);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Sexo_Feminino_Deve_Ser_F()
        {
            var expected = ResultCodes.Success;
            var result = Validate.Sexo("F");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Sexo_Feminino_Deve_Ser_F_Mesmo_Não_Sendo_Obrigatório()
        {
            var expected = ResultCodes.Success;
            var result = Validate.Sexo("F", false);
            Assert.AreEqual(expected, result);
        }
    }
}
