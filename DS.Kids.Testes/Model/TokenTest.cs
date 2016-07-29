using DS.Kids.Model;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DS.Kids.Testes.Model
{
    [TestClass]
    public class TokenTest
    {
        [TestMethod]
        public void Criar_Token()
        {
            var value = new Responsavel { IdResponsavel = 1 };
            var result = Token.Criar(value);
            Assert.IsNotNull(result.Valor);
        }
    }
}
