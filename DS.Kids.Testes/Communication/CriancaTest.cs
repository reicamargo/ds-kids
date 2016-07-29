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
    public class CriancaTest
    {

        [TestInitialize]
        public void Initialize()
        {
            Authorization.Singleton.KillToken();
        }

        [TestMethod]
        public void Inserir_Crianca()
        {
            var expected = ResultCodes.Success;
            var result = Criar();
            var resultCrianca = InserirCrianca(result.Data.IdResponsavel);

            Authorization.Singleton.KillToken();

            Assert.AreEqual(expected, result.ResultCode);
            Assert.AreEqual(expected, resultCrianca.ResultCode);
            Assert.AreEqual(1, resultCrianca.Data.Crescimentos.Count);
        }

        [TestMethod]
        [StatusCodeExpected(HttpStatusCode.Forbidden)]
        public void Inserir_Crianca_Deslogado()
        {
            var result = Criar();
            Authorization.Singleton.KillToken();
            var resultCrianca = InserirCrianca(result.Data.IdResponsavel);
        }

        [TestMethod]
        public void Atualizar_Crianca()
        {
            var expected = ResultCodes.Success;
            var result = Criar();
            var resultInserir = InserirCrianca(result.Data.IdResponsavel);
            var criancaAtualizar = resultInserir.Data;
            criancaAtualizar.Crescimentos = null;
            criancaAtualizar.Nome = Util.CreateString(8);
            var resultAtualizar = AtualizarCrianca(criancaAtualizar);
            
            Authorization.Singleton.KillToken();

            Assert.AreEqual(expected, result.ResultCode);
            Assert.AreEqual(expected, resultInserir.ResultCode);
            Assert.AreEqual(expected, resultAtualizar.ResultCode);
        }

        [TestMethod]
        [StatusCodeExpected(HttpStatusCode.Forbidden)]
        public void Atualizar_Crianca_Deslogado()
        {
            var result = Criar();
            var resultInserir = InserirCrianca(result.Data.IdResponsavel);
            var criancaAtualizar = resultInserir.Data;
            criancaAtualizar.Crescimentos = null;
            criancaAtualizar.Nome = Util.CreateString(8);
            Authorization.Singleton.KillToken();
            var resultAtualizar = AtualizarCrianca(criancaAtualizar);
        }

        [TestMethod]
        public void Excluir_Crianca()
        {
            var expected = ResultCodes.Success;
            var result = Criar();
            var resultCrianca = InserirCrianca(result.Data.IdResponsavel);
            var resultExclusao = ExcluirCrianca(resultCrianca.Data.IdCrianca);

            Authorization.Singleton.KillToken();

            Assert.AreEqual(expected, result.ResultCode);
            Assert.AreEqual(expected, resultCrianca.ResultCode);
            Assert.AreEqual(expected, resultExclusao.ResultCode); 
        }

        [TestMethod]
        [StatusCodeExpected(HttpStatusCode.Forbidden)]
        public void Excluir_Crianca_Deslogado()
        {
            var result = Criar();
            var resultCrianca = InserirCrianca(result.Data.IdResponsavel);
            Authorization.Singleton.KillToken();
            var resultExclusao = ExcluirCrianca(resultCrianca.Data.IdCrianca);
        }

        private Result<Responsavel> Criar()
        {
            var responsavel = new Responsavel
            {
                Email = Util.CreateEmail(),
                Nome = Util.CreateString(8),
                Senha = Util.CreateString(6),
                Telefone = "1967739788"
            };

            var communication = new Responsaveis();
            return communication.InserirAsync(responsavel).Result;
        }

        private Result<Crianca> InserirCrianca(int responsavelId)
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

            var communication = new Criancas();
            return communication.InserirAsync(crianca).Result;
        }

        private Result<Crianca> AtualizarCrianca(Crianca crianca)
        {
            var communication = new Criancas();
            return communication.AtualizarAsync(crianca).Result;
        }

        private Result ExcluirCrianca(int id)
        {
            var communication = new Criancas();
            return communication.ExcluirAsync(id).Result;
        }
    }
}
