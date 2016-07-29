using System;
using System.Net;

using DS.Kids.Model;
using DS.Kids.Model.Communication;
using DS.Kids.Model.Validations;
using DS.Kids.Testes.Infra;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Authorization = DS.Kids.Model.Communication.Authorization;

namespace DS.Kids.Testes.Communication
{
    [TestClass]
    public class CrescimentoTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Authorization.Singleton.KillToken();
        }

        [TestMethod]
        public void Inserir_Crescimento()
        {
            var responsavel = CriarEntidadeResponsavel();

            var expected = ResultCodes.Success;
            var resultResponsavel = CriarResponsavel(responsavel);
            var resultCrianca = AdicionarCrianca(resultResponsavel.Data.IdResponsavel);
            var resultCrescimento = InserirCrescimento(resultCrianca.Data.IdCrianca);

            Authorization.Singleton.KillToken();

            Assert.AreEqual(expected, resultResponsavel.ResultCode);
            Assert.AreEqual(expected, resultCrianca.ResultCode);
            Assert.AreEqual(expected, resultCrescimento.ResultCode);
        }

        [TestMethod]
        [StatusCodeExpected(HttpStatusCode.Forbidden)]
        public void Inserir_Crescimento_Deslogado()
        {
            var responsavel = CriarEntidadeResponsavel();
            var resultResponsavel = CriarResponsavel(responsavel);
            var resultCrianca = AdicionarCrianca(resultResponsavel.Data.IdResponsavel);
            Authorization.Singleton.KillToken();
            var resultCrescimento = InserirCrescimento(resultCrianca.Data.IdCrianca);
        }

        [TestMethod]
        public void Atualizar_Crescimento()
        {
            var responsavel = CriarEntidadeResponsavel();

            var expected = ResultCodes.Success;
            var resultResponsavel = CriarResponsavel(responsavel);
            var resultCrianca = AdicionarCrianca(resultResponsavel.Data.IdResponsavel);
            var resultCrescimento = InserirCrescimento(resultCrianca.Data.IdCrianca);
            var resultAtualizar = AtualizarCrescimento(resultCrescimento.Data.IdCrescimento, resultCrianca.Data.IdCrianca);

            Authorization.Singleton.KillToken();

            Assert.AreEqual(expected, resultAtualizar.ResultCode);
            Assert.AreEqual(expected, resultResponsavel.ResultCode);
            Assert.AreEqual(expected, resultCrianca.ResultCode);
            Assert.AreEqual(expected, resultCrescimento.ResultCode);
            Assert.AreEqual(resultAtualizar.Data.Altura, 1.80m);
            Assert.AreEqual(resultAtualizar.Data.Peso, 40m);
        }

        [TestMethod]
        [StatusCodeExpected(HttpStatusCode.Forbidden)]
        public void Atualizar_Crescimento_Deslogado()
        {
            var responsavel = CriarEntidadeResponsavel();
            var resultResponsavel = CriarResponsavel(responsavel);
            var resultCrianca = AdicionarCrianca(resultResponsavel.Data.IdResponsavel);
            var resultCrescimento = InserirCrescimento(resultCrianca.Data.IdCrianca);
            Authorization.Singleton.KillToken();
            var resultAtualizar = AtualizarCrescimento(resultCrescimento.Data.IdCrescimento, resultCrianca.Data.IdCrianca);
        }

        private Result<Responsavel> CriarResponsavel(Responsavel responsavel)
        {
            var communication = new Responsaveis();
            return communication.InserirAsync(responsavel).Result;
        }

        private Result<Crianca> AdicionarCrianca(int responsavelId)
        {
            var crianca = CriarEntidadeCrianca(responsavelId);

            var communication = new Criancas();
            return communication.InserirAsync(crianca).Result;
        }

        private Result<Crescimento> InserirCrescimento(int criancaId)
        {
            var pesoAltura = new PesoAltura { IdCrianca = criancaId, Altura = 1.50m, Peso = 30m };

            var communication = new Crescimentos();
            return communication.InserirAsync(pesoAltura).Result;
        }

        private Result<Crescimento> AtualizarCrescimento(int idCrescimento, int idCrianca)
        {
            var pesoAltura = new PesoAltura { IdCrescimento = idCrescimento, IdCrianca = idCrianca, Altura = 1.80m, Peso = 40m };

            var communication = new Crescimentos();
            return communication.AtualizarAsync(pesoAltura).Result;
        }

        private Responsavel CriarEntidadeResponsavel()
        {
            var responsavel = new Responsavel
            {
                Email = Util.CreateEmail(),
                Nome = Util.CreateString(8),
                Senha = Util.CreateString(6),
                Telefone = "1967739788"
            };

            return responsavel;
        }

        private Crianca CriarEntidadeCrianca(int responsavelId)
        {
            var crianca = new Crianca
            {
                IdResponsavel = responsavelId,
                AlturaInicial = 1.5m,
                PesoInicial = 50.5m,
                Sexo = "M",
                Nome = Util.CreateString(10),
                DataNascimento = DateTime.Now.AddYears(-4)
            };

            return crianca;
        }
    }
}
