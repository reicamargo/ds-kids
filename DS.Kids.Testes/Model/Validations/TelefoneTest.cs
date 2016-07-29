using DS.Kids.Model.Validations;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DS.Kids.Testes.Model.Validations
{
    [TestClass]
    public class TelefoneTest
    {
        [TestMethod]
        public void Telefone_Deve_Ser_Obrigatório()
        {
            var expected = ResultCodes.TelefoneObrigatorio;
            var result = Validate.Telefone("");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Telefone_Deve_Conter_Menos_Do_Que_11_Carecteres()
        {
            var expected = ResultCodes.TelefoneInvalido;
            var result = Validate.Telefone(new string('1', 12));
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Telefone_Deve_Conter_Menos_Do_Que_11_Carecteres_E_Ser_Obrigatório()
        {
            var expected = ResultCodes.Success;
            var result = Validate.Telefone("11912345678");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Telefone_Deve_Conter_Menos_Do_Que_11_Carecteres_Mesmo_Não_Sendo_Obrigatório()
        {
            var expected = ResultCodes.TelefoneInvalido;
            var result = Validate.Telefone(new string('1', 12), false);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Telefone_Deve_Conter_Menos_Do_Que_11_Carecteres_Mesmo_Não_Sendo_Obrigatório_Mas_Contendo_Um_Valor_Válido()
        {
            var expected = ResultCodes.Success;
            var result = Validate.Telefone("11912345678", false);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Telefone_Deve_Conter_Sequência_Válida_De_Dígitos()
        {
            var expected = ResultCodes.TelefoneInvalido;
            var result = Validate.Telefone("11111111111");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Telefone_Deve_Conter_Sequência_Válida_De_Dígitos_Mesmo_Não_Sendo_Obrigatório()
        {
            var expected = ResultCodes.TelefoneInvalido;
            var result = Validate.Telefone("11111111111", false);
            Assert.AreEqual(expected, result);
        }
    }
}
