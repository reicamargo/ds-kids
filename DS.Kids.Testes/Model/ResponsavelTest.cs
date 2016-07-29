using System;

using DS.Kids.Model;
using DS.Kids.Model.Validations;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DS.Kids.Testes.Model
{
    [TestClass]
    public class ResponsavelTest
    {
        [TestMethod]
        public void Cria_Responsavel_Valido()
        {
            var value = new Responsavel
            {
                Email = "angelo@email.com.br",
                Nome = "Angelo Luis",
                Senha = "123456",
                Telefone = "11987654321"
            };
            var expected = ResultCodes.Success;
            var result = value.Validate();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Cria_Responsavel_Com_Email_Inválido()
        {
            var value = new Responsavel
            {
                Email = "",
                Nome = "Angelo Luis",
                Senha = "123456",
                Telefone = "11987654321"
            };
            var expected = ResultCodes.EmailObrigatorio;
            var result = value.Validate();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Cria_Responsavel_Com_Nome_Inválido()
        {
            var value = new Responsavel
            {
                Email = "angelo@email.com.br",
                Nome = "",
                Senha = "123456",
                Telefone = "11987654321"
            };
            var expected = ResultCodes.NomeObrigatorio;
            var result = value.Validate();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Cria_Responsavel_Senha_Inválida()
        {
            var value = new Responsavel
            {
                Email = "angelo@email.com.br",
                Nome = "Angelo Luis",
                Senha = "",
                Telefone = "11987654321"
            };
            var expected = ResultCodes.SenhaObrigatoria;
            var result = value.Validate();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Cria_Responsavel_Com_Telefone_Inválido()
        {
            var value = new Responsavel
            {
                Email = "angelo@email.com.br",
                Nome = "Angelo Luis",
                Senha = "123456",
                Telefone = "11111111111"
            };
            var expected = ResultCodes.TelefoneInvalido;
            var result = value.Validate();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Valida_Insercao_Crianca_Com_Nome_Ja_Cadastrado_No_Responsavel()
        {
            var responsavel = new Responsavel
            {
                Email = "angelo@email.com.br",
                Nome = "Angelo Luis",
                Senha = "123456"
            };
            var expected = ResultCodes.Success;
            var result = responsavel.Validate();
            Assert.AreEqual(expected, result);

            responsavel.Criancas.Add(new Crianca
            {
                Nome = "João",
                Responsavel = responsavel,
                IdResponsavel = responsavel.IdResponsavel,
                Sexo = "M",
                AlturaInicial = 1.5m,
                PesoInicial = 60m,
                DataNascimento = DateTime.Now.AddYears(-3)
            });

            responsavel.Criancas.Add(new Crianca
            {
                Nome = "Maria",
                Responsavel = responsavel,
                IdResponsavel = responsavel.IdResponsavel,
                Sexo = "F",
                AlturaInicial = 1.4m,
                PesoInicial = 45m,
                DataNascimento = DateTime.Now.AddYears(-3)
            });

            var crianca = new Crianca
            {
                Nome = "João",
                Responsavel = responsavel,
                IdResponsavel = responsavel.IdResponsavel,
                Sexo = "M",
                AlturaInicial = 1.3m,
                PesoInicial = 52m,
                DataNascimento = DateTime.Now.AddYears(-6)
            };

            var expectedInsercao = ResultCodes.CriancaJaCadastrada;
            var resultInsercao = responsavel.ValidarInsercaoCrianca(crianca);
            Assert.AreEqual(expectedInsercao, resultInsercao);
        }

        [TestMethod]
        public void Valida_Insercao_Crianca_Com_Nome_Nao_Cadastrado_No_Responsavel()
        {
            var responsavel = new Responsavel
            {
                Email = "angelo@email.com.br",
                Nome = "Angelo Luis",
                Senha = "123456"
            };
            var expected = ResultCodes.Success;
            var result = responsavel.Validate();
            Assert.AreEqual(expected, result);

            responsavel.Criancas.Add(new Crianca
            {
                Nome = "João",
                Responsavel = responsavel,
                IdResponsavel = responsavel.IdResponsavel,
                Sexo = "M",
                AlturaInicial = 1.5m,
                PesoInicial = 60m,
                DataNascimento = DateTime.Now.AddYears(-3)
            });

            responsavel.Criancas.Add(new Crianca
            {
                Nome = "Maria",
                Responsavel = responsavel,
                IdResponsavel = responsavel.IdResponsavel,
                Sexo = "F",
                AlturaInicial = 1.4m,
                PesoInicial = 45m,
                DataNascimento = DateTime.Now.AddYears(-3)
            });

            var crianca = new Crianca
            {
                Nome = "Pedro",
                Responsavel = responsavel,
                IdResponsavel = responsavel.IdResponsavel,
                Sexo = "M",
                AlturaInicial = 1.3m,
                PesoInicial = 52m,
                DataNascimento = DateTime.Now.AddYears(-6)
            };

            var expectedInsercao = ResultCodes.Success;
            var resultInsercao = responsavel.ValidarInsercaoCrianca(crianca);
            Assert.AreEqual(expectedInsercao, resultInsercao);
        }
    }
}
