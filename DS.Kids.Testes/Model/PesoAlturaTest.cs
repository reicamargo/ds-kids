using DS.Kids.Model;
using DS.Kids.Model.Validations;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DS.Kids.Testes.Model
{
    [TestClass]
    public class PesoAlturaTest
    {
        [TestMethod]
        public void Obter_IMC_Peso_Altura()
        {
            var expected = 27m;
            var result = PesoAltura.ObterImc(80, 1.70m);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Obter_IMC_PesoAltura()
        {
            var value = new PesoAltura { Peso = 80, Altura = 1.70m };
            var expected = 27m;
            var result = value.ObterImc();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Atualizar_Peso_Ultimo_Crescimento()
        {
            var pesoAltura = new PesoAltura { Altura = 1.5m, Peso = null };
            var crescimento = new Crescimento { Altura = 1.6m, Peso = 40m };

            pesoAltura.AtualizarPesoAlturaUltimoCrescimento(crescimento);
            Assert.AreEqual(pesoAltura.Peso, crescimento.Peso);
            Assert.AreNotEqual(pesoAltura.Altura, crescimento.Altura);
        }

        [TestMethod]
        public void Atualizar_Altura_Ultimo_Crescimento()
        {
            var pesoAltura = new PesoAltura { Altura = null, Peso = 40m };
            var crescimento = new Crescimento { Altura = 1.6m, Peso = 45m };

            pesoAltura.AtualizarPesoAlturaUltimoCrescimento(crescimento);
            Assert.AreEqual(pesoAltura.Altura, crescimento.Altura);
            Assert.AreNotEqual(pesoAltura.Peso, crescimento.Peso);
        }

        [TestMethod]
        public void Atualizar_Peso_Altura_Ultimo_Crescimento()
        {
            var pesoAltura = new PesoAltura { Altura = null, Peso = null };
            var crescimento = new Crescimento { Altura = 1.6m, Peso = 40m };

            pesoAltura.AtualizarPesoAlturaUltimoCrescimento(crescimento);
            Assert.AreEqual(pesoAltura.Altura, crescimento.Altura);
            Assert.AreEqual(pesoAltura.Peso, crescimento.Peso);
        }

        [TestMethod]
        public void Atualizar_Peso_Altura_Sem_Crescimento()
        {
            var pesoAltura = new PesoAltura { Altura = 1.5m, Peso = 40m};

            pesoAltura.AtualizarPesoAlturaUltimoCrescimento(null);
            Assert.AreEqual(pesoAltura.Altura, 1.5m);
            Assert.AreEqual(pesoAltura.Peso, 40m);
        }

        [TestMethod]
        public void Peso_Altura_Com_Valores_Invalidos()
        {
            var expected = ResultCodes.PesoAlturaInvalidos;
            var pesoAltura = new PesoAltura();
            var result = pesoAltura.Validate();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Peso_Com_Valor_Invalido()
        {
            var expected = ResultCodes.PesoInvalido;
            var pesoAltura = new PesoAltura { Peso = -13m, Altura = 1.5m };
            var result = pesoAltura.Validate();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Altura_Com_Valor_Invalido()
        {
            var expected = ResultCodes.AlturaInvalida;
            var pesoAltura = new PesoAltura { Peso = 29.9m, Altura = -1.8m };
            var result = pesoAltura.Validate();
            Assert.AreEqual(expected, result);
        }
    }
}
