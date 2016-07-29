using DS.Kids.Model.Validations;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DS.Kids.Testes.Model.Validations
{
    [TestClass]
    public class EmailTest
    {
        [TestMethod]
        public void Email_Deve_Ser_Obrigatório()
        {
            var expected = ResultCodes.EmailObrigatorio;
            var result = Validate.Email("");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Email_Deve_Conter_Menos_Do_Que_80_Carecteres()
        {
            var value = string.Format("email@{0}.com.br", new string('x', 80));

            var expected = ResultCodes.TamanhoMaximoCampoEmail;
            var result = Validate.Email(value);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Email_Deve_Conter_Menos_Do_Que_80_Carecteres_E_Ser_Obrigatório()
        {
            var expected = ResultCodes.Success;
            var result = Validate.Email("email@servidor.com.br");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Email_Deve_Conter_Menos_Do_Que_80_Carecteres_Mesmo_Não_Sendo_Obrigatório()
        {
            var value = string.Format("email@{0}.com.br", new string('x', 80));

            var expected = ResultCodes.TamanhoMaximoCampoEmail;
            var result = Validate.Email(value, false);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Email_Deve_Conter_Menos_Do_Que_80_Carecteres_Mesmo_Não_Sendo_Obrigatório_Mas_Contendo_Um_Valor_Válido()
        {
            var expected = ResultCodes.Success;
            var result = Validate.Email("email@servidor.com.br", false);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Email_Deve_Ser_Valido()
        {
            var expected = ResultCodes.EmailInvalido;
            var result = Validate.Email("email inválido");
            Assert.AreEqual(expected, result);
        }
    }
}
