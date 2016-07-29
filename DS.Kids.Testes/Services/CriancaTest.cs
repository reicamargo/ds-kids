using System;
using System.Linq;

using DS.Kids.Model.Repositories;
using DS.Kids.Model.Services;
using DS.Kids.Model.Validations;
using DS.Kids.Testes.Infra;
using DS.Kids.Testes.Services.__Fakes.Repositories;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DS.Kids.Testes.Services
{
    [TestClass]
    public class CriancaTest
    {
        private ICriancas _criancas;
        private IResponsaveis _responsaveis;
        private ICrescimentos _crescimentos;
        private Crianca _service;

        [TestInitialize]
        public void Initialize()
        {
            var database = new Database();
            _criancas = new CriancasFake(database);
            _responsaveis = new ResponsaveisFake(database);
            _crescimentos = new CrescimentosFake(database);
            _service = new Crianca(_criancas, _responsaveis, _crescimentos);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Inserir_Crianca_Com_Entidade_Nula()
        {
            try
            {
                var x = _service.InserirAsync(null).Result;
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }

        [TestMethod]
        public void Inserir_Crianca_Com_Informacoes_Invalidas()
        {
            var crianca = new Kids.Model.Crianca();

            var expected = ResultCodes.Success;
            var result = _service.InserirAsync(crianca).Result;
            Assert.AreNotEqual(expected, result.ResultCode);
        }

        [TestMethod]
        public void Inserir_Crianca_Com_Responsavel_Inexistente()
        {
            var crianca = new Kids.Model.Crianca
            {
                IdResponsavel = 9999,
                AlturaInicial = 1.5m,
                PesoInicial = 50.5m,
                Sexo = "M",
                Nome = Util.CreateString(10),
                DataNascimento = DateTime.Now.AddYears(-4)
            };

            var expected = ResultCodes.ResponsavelNaoEncontrado;
            var result = _service.InserirAsync(crianca).Result;
            Assert.AreEqual(expected, result.ResultCode);
        }

        [TestMethod]
        public void Inserir_Crianca_Com_Nome_Ja_Cadastrado()
        {
            var crianca = new Kids.Model.Crianca
            {
                IdResponsavel = 1,
                AlturaInicial = 1.5m,
                PesoInicial = 50.5m,
                Sexo = "M",
                Nome = "Otávio",
                DataNascimento = DateTime.Now.AddYears(-4)
            };

            var expected = ResultCodes.CriancaJaCadastrada;
            var result = _service.InserirAsync(crianca).Result;
            Assert.AreEqual(expected, result.ResultCode);
        }

        [TestMethod]
        public void Inserir_Crianca_Com_Informacoes_Validas()
        {
            var crianca = new Kids.Model.Crianca
            {
                IdResponsavel = 1,
                AlturaInicial = 1.5m,
                PesoInicial = 50.5m,
                Sexo = "M",
                Nome = Util.CreateString(8),
                DataNascimento = DateTime.Now.AddYears(-4)
            };

            var expected = ResultCodes.Success;
            var result = _service.InserirAsync(crianca).Result;
            Assert.AreEqual(expected, result.ResultCode);
        }

        [TestMethod]
        public void Inserir_Crianca_Deve_Inserir_Crescimento()
        {
            var crianca = new Kids.Model.Crianca
            {
                IdResponsavel = 1,
                AlturaInicial = 1.5m,
                PesoInicial = 50.5m,
                Sexo = "M",
                Nome = Util.CreateString(8),
                DataNascimento = DateTime.Now.AddYears(-4)
            };

            var expected = ResultCodes.Success;
            var result = _service.InserirAsync(crianca).Result;
            Assert.AreEqual(expected, result.ResultCode);
            Assert.AreEqual(1, crianca.Crescimentos.Count());
        }
    }
}
