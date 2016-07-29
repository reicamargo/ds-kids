using DS.Kids.Model.Communication;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DS.Kids.Testes.Communication
{
    [TestClass]
    public class AuthorizationTest
    {
        [TestMethod]
        public void Set_Token_Valor_Correto()
        {
            Authorization.Singleton.SetToken("Token");
            var expected = Authorization.Singleton.GetToken();
            Assert.AreEqual("Token", expected);
            Authorization.Singleton.KillToken();
        }

        [TestMethod]
        public void Kill_Token()
        {
            Authorization.Singleton.SetToken("Token");
            Authorization.Singleton.KillToken();
            var result = Authorization.Singleton.GetToken();
            Assert.IsNull(result);
        }
    }
}
