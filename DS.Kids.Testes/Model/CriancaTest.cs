using System;

using DS.Kids.Model;
using DS.Kids.Model.Validations;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DS.Kids.Testes.Model
{
    [TestClass]
    public class CriancaTest
    {
        [TestMethod]
        public void Cria_Crianca_Com_Informacoes_Validas()
        {
            var value = new Crianca
            {
                AlturaInicial = 1.50m,
                DataNascimento = DateTime.Now.AddYears(-5),
                Nome = "Pedro Otávio",
                PesoInicial = 20m,
                IdResponsavel = 1,
                Sexo = "M"
            };
            var expected = ResultCodes.Success;
            var result = value.Validate();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Cria_Criança_Com_Nome_Inválido()
        {
            var value = new Crianca
            {
                AlturaInicial = 150m,
                DataNascimento = DateTime.Now.AddYears(-5),
                Nome = "",
                PesoInicial = 20m,
                IdResponsavel = 1,
                Sexo = "M"
            };
            var expected = ResultCodes.NomeObrigatorio;
            var result = value.Validate();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Cria_Criança_Com_Data_De_Nascimento_Inválida()
        {
            var value = new Crianca
            {
                AlturaInicial = 150m,
                DataNascimento = DateTime.Now.AddYears(-1),
                Nome = "Pedro Otávio",
                PesoInicial = 20m,
                IdResponsavel = 1,
                Sexo = "M"
            };
            var expected = ResultCodes.DataNascimentoInvalidaCrianca;
            var result = value.Validate();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Cria_Crianca_Com_Sexo_Inválido()
        {
            var value = new Crianca
            {
                AlturaInicial = 150m,
                DataNascimento = DateTime.Now.AddYears(-5),
                Nome = "Pedro Otávio",
                PesoInicial = 20m,
                IdResponsavel = 1,
                Sexo = "X"
            };
            var expected = ResultCodes.SexoInvalido;
            var result = value.Validate();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Cria_Crianca_Com_Peso_Inicial_Inválido()
        {
            var value = new Crianca
            {
                AlturaInicial = 150m,
                DataNascimento = DateTime.Now.AddYears(-5),
                Nome = "Pedro Otávio",
                PesoInicial = 1m,
                IdResponsavel = 1,
                Sexo = "M"
            };
            var expected = ResultCodes.PesoInvalido;
            var result = value.Validate();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Cria_Crianca_Com_Altura_Inicial_Inválida()
        {
            var value = new Crianca
            {
                AlturaInicial = 201m,
                DataNascimento = DateTime.Now.AddYears(-5),
                Nome = "Pedro Otávio",
                PesoInicial = 8m,
                IdResponsavel = 1,
                Sexo = "M"
            };
            var expected = ResultCodes.AlturaInvalida;
            var result = value.Validate();
            Assert.AreEqual(expected, result);
        }
    }
}
