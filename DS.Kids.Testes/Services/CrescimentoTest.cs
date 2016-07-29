using System;
using System.Linq;

using DS.Kids.Model;
using DS.Kids.Model.Validations;
using DS.Kids.Testes.Infra;
using DS.Kids.Testes.Services.__Fakes.Repositories;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Crescimento = DS.Kids.Model.Services.Crescimento;
using Crianca = DS.Kids.Model.Services.Crianca;

namespace DS.Kids.Testes.Services
{
    [TestClass]
    public class CrescimentoTest
    {
        private CrescimentosFake _crescimentos;

        private CriancasFake _criancas;

        private ResponsaveisFake _responsaveis;

        [TestInitialize]
        public void Initialize()
        {
            var database = new Database();
            _crescimentos = new CrescimentosFake(database);
            _criancas = new CriancasFake(database);
            _responsaveis = new ResponsaveisFake(database);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Criar_Crescimento_Com_Repositorio_Crianca_Nulo_Deve_Gerar_Exceção()
        {
            var service = new Crescimento(null, _crescimentos);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Criar_Crescimento_Com_Repositorio_Crescimento_Nulo_Deve_Gerar_Exceção()
        {
            var service = new Crescimento(_criancas, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Inserir_Crescimento_Com_Peso_Altura_Nulo_Deve_Gerar_Exceção()
        {
            try
            {
                var service = new Crescimento(_criancas, _crescimentos);
                var result = service.InserirAsync(null).Result;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Atualizar_Crescimento_Com_Peso_Altura_Nulo_Deve_Gerar_Exceção()
        {
            try
            {
                var service = new Crescimento(_criancas, _crescimentos);
                var result = service.AtualizarAsync(null).Result;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        [TestMethod]
        public void Inserir_Crescimento_Com_Peso_Altura_Invalido_Deve_Result_Error()
        {
            var service = new Crescimento(_criancas, _crescimentos);
            var pesoAltura = new PesoAltura();
            var expected = ResultCodes.PesoAlturaInvalidos;
            var result = service.InserirAsync(pesoAltura).Result;
            Assert.AreEqual(expected, result.ResultCode);
        }

        [TestMethod]
        public void Atualizar_Crescimento_Com_Peso_Altura_Invalido_Deve_Result_Error()
        {
            var service = new Crescimento(_criancas, _crescimentos);
            var pesoAltura = new PesoAltura();
            var expected = ResultCodes.PesoAlturaInvalidos;
            var result = service.AtualizarAsync(pesoAltura).Result;
            Assert.AreEqual(expected, result.ResultCode);
        }

        [TestMethod]
        public void Inserir_Crescimento_Deve_Adicionar_Um_Crescimento_A_Uma_Crianca()
        {
            var service = new Crescimento(_criancas, _crescimentos);
            var serviceCrianca = new Crianca(_criancas, _responsaveis, _crescimentos);

            var responsavel = _responsaveis.ObterPorIdAsync(1).Result;
            var crianca = new Kids.Model.Crianca
            {
                AlturaInicial = 1.40m,
                PesoInicial = 29.9m,
                IdResponsavel = responsavel.IdResponsavel,
                Nome = Util.CreateString(10),
                Sexo = "M",
                DataNascimento = DateTime.Now.AddYears(-4)
            };
            serviceCrianca.InserirAsync(crianca).Wait();
            Assert.AreEqual(1, crianca.Crescimentos.Count);

            var pesoAltura = new PesoAltura
            {
                Altura = 1.40m,
                Peso = 29.9m,
                IdCrianca = crianca.IdCrianca
            };

            var expected = ResultCodes.Success;
            var result = service.InserirAsync(pesoAltura).Result;
            Assert.AreEqual(expected, result.ResultCode);

            var crescimentosPorCrianca = _crescimentos.ListarPorCriancaIdAsync(crianca.IdCrianca).Result;
            Assert.AreEqual(2, crescimentosPorCrianca.Count());
        }

        [TestMethod]
        public void Atualizar_Crescimento_Deve_Atualizar_Um_Crescimento_De_Uma_Crianca()
        {
            var service = new Crescimento(_criancas, _crescimentos);
            var serviceCrianca = new Crianca(_criancas, _responsaveis, _crescimentos);

            var responsavel = _responsaveis.ObterPorIdAsync(1).Result;
            var crianca = new Kids.Model.Crianca
            {
                AlturaInicial = 1.40m,
                PesoInicial = 29.9m,
                IdResponsavel = responsavel.IdResponsavel,
                Nome = Util.CreateString(10),
                Sexo = "M",
                DataNascimento = DateTime.Now.AddYears(-4)
            };
            serviceCrianca.InserirAsync(crianca).Wait();
            Assert.AreEqual(1, crianca.Crescimentos.Count);

            var pesoAltura = new PesoAltura
            {
                Altura = 1.40m,
                Peso = 29.9m,
                IdCrianca = crianca.IdCrianca
            };

            var expected = ResultCodes.Success;
            var result = service.InserirAsync(pesoAltura).Result;
            Assert.AreEqual(expected, result.ResultCode);

            var crescimentosPorCrianca = _crescimentos.ListarPorCriancaIdAsync(crianca.IdCrianca).Result;
            var crescimentoDaCrianca = _crescimentos.ObterUltimoRegistroDeCrescimentoPorCriancaIdAsync(crianca.IdCrianca).Result;
            var pesoAlturaAtualizacao = new PesoAltura
            {
                Altura = 1.50m,
                Peso = 32.9m,
                IdCrianca = crianca.IdCrianca,
                IdCrescimento = crescimentoDaCrianca.IdCrescimento
            };
            result = service.AtualizarAsync(pesoAlturaAtualizacao).Result;
            Assert.AreEqual(expected, result.ResultCode);

            crescimentosPorCrianca = _crescimentos.ListarPorCriancaIdAsync(crianca.IdCrianca).Result;
            crescimentoDaCrianca = _crescimentos.ObterUltimoRegistroDeCrescimentoPorCriancaIdAsync(crianca.IdCrianca).Result;

            Assert.AreEqual(1.50m, crescimentoDaCrianca.Altura);
            Assert.AreEqual(32.9m, crescimentoDaCrianca.Peso);
        }
    }
}