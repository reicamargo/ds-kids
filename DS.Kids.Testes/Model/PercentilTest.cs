using DS.Kids.Model;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DS.Kids.Testes.Model
{
    [TestClass]
    public class PercentilTest
    {
        [TestMethod]
        public void Masculino_Desnutricao()
        {
            var expected = TipoCrescimento.Desnutricao;
            var result = Percentil.ObterTipoCrescimento("M", 30, 12.8m);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Masculino_Normal()
        {
            var expected = TipoCrescimento.Normal;
            var result = Percentil.ObterTipoCrescimento("M", 24, 14.3m);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Masculino_Sobrepeso()
        {
            var expected = TipoCrescimento.Sobrepeso;
            var result = Percentil.ObterTipoCrescimento("M", 65, 19.0m);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Masculino_Obesidade()
        {
            var expected = TipoCrescimento.Obesidade;
            var result = Percentil.ObterTipoCrescimento("M", 30, 30.8m);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Feminino_Desnutricao()
        {
            var expected = TipoCrescimento.Desnutricao;
            var result = Percentil.ObterTipoCrescimento("F", 30, 12.3m);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Feminino_Normal()
        {
            var expected = TipoCrescimento.Normal;
            var result = Percentil.ObterTipoCrescimento("F", 24, 15.0m);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Feminino_Sobrepeso()
        {
            var expected = TipoCrescimento.Sobrepeso;
            var result = Percentil.ObterTipoCrescimento("F", 25, 19.0m);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Feminino_Obesidade()
        {
            var expected = TipoCrescimento.Obesidade;
            var result = Percentil.ObterTipoCrescimento("F", 30, 32.8m);
            Assert.AreEqual(expected, result);
        }
    }
}
