using System;

using DS.Kids.Model;
using DS.Kids.Model.Validations;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DS.Kids.Testes.Model
{
    [TestClass]
    public class CrescimentoTest
    {
        [TestMethod]
        public void Cria_Crescimento_Com_Valores_Validos()
        {
            var value = new Crescimento { Peso = 30, Altura = 1.50m};
            var expected = ResultCodes.Success;
            var result = value.Validate();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Cria_Crescimento_Com_Peso_Invalido()
        {
            var value = new Crescimento { Peso = 0, Altura = 150m };
            var expected = ResultCodes.PesoInvalido;
            var result = value.Validate();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Cria_Crescimento_Com_Altura_Invalida()
        {
            var value = new Crescimento { Peso = 30, Altura = 201m };
            var expected = ResultCodes.AlturaInvalida;
            var result = value.Validate();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Criar_Crescimento_Com_Crianca_Peso_Altura()
        {
            var responsavel = new Responsavel
            {
                Email = "angelo@email.com.br",
                Nome = "Angelo Luis",
                Senha = "123456"
            };

            var crianca = new Crianca
            {
                IdCrianca = 444,
                Nome = "João",
                Responsavel = responsavel,
                IdResponsavel = responsavel.IdResponsavel,
                Sexo = "M",
                AlturaInicial = 1.3m,
                PesoInicial = 52m,
                DataNascimento = DateTime.Now.AddYears(-6)
            };

            var pesoAltura = new PesoAltura { Altura = 1.5m, Peso = 40m };
            var crescimento = new Crescimento(crianca, pesoAltura);

            var mesesDeIdade = crianca.DataNascimento.GetTotalMonths();
            var imc = pesoAltura.ObterImc();
            var tipoCrescimento = Percentil.ObterTipoCrescimento(crianca.Sexo, mesesDeIdade, imc);

            Assert.AreEqual(crescimento.Altura, pesoAltura.Altura);
            Assert.AreEqual(crescimento.Peso, pesoAltura.Peso);
            Assert.AreEqual(crescimento.IdCrianca, crianca.IdCrianca);
            Assert.AreEqual(crescimento.MesesDeIdade, crianca.DataNascimento.GetTotalMonths());
            Assert.AreEqual(crescimento.TipoCrescimento, tipoCrescimento);
        }
    }
}
