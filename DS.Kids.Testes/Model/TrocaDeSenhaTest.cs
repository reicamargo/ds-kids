using DS.Kids.Model;
using DS.Kids.Model.Validations;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DS.Kids.Testes.Model
{
    [TestClass]
    public class TrocaDeSenhaTest
    {
        [TestMethod]
        public void Cria_Troca_Senha_Valida()
        {
            var value = new TrocaDeSenha
            {
                SenhaAtual = "123456",
                NovaSenha = "456789"
            };
            var expected = ResultCodes.Success;
            var result = value.Validate();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Cria_Troca_Senha_Informacoes_Inválida()
        {
            var value = new TrocaDeSenha
            {
                SenhaAtual = "123456",
                NovaSenha = "123456"
            };
            var expected = ResultCodes.NovaSenhaIgualSenhaAtual;
            var result = value.Validate();
            Assert.AreEqual(expected, result);
        }
    }
}
