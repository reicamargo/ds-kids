﻿using System.Net;

using DS.Kids.Model;
using DS.Kids.Model.Communication;
using DS.Kids.Model.Validations;
using DS.Kids.Testes.Infra;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Authorization = DS.Kids.Model.Communication.Authorization;

namespace DS.Kids.Testes.Communication
{
    [TestClass]
    public class DicasTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Authorization.Singleton.KillToken();
        }

        [TestMethod]
        public void Obter_Dica_Por_Id()
        {
            var expected = ResultCodes.Success;
            
            var responsavel = new Responsavel
            {
                Email = Util.CreateEmail("Troca_De_Senha"),
                Nome = Util.CreateString(8),
                Senha = Util.CreateString(6),
                Telefone = "1967739788"
            };

            var responsavelResult = Criar(responsavel);
            Assert.AreEqual(expected, responsavelResult.ResultCode);
            var communication = new Dicas();
            var result = communication.ObterPorIdAsync(1).Result;
            Authorization.Singleton.KillToken();
            Assert.AreEqual(expected, result.ResultCode);
            Assert.IsNotNull(result.Data);
        }

        private Result<Responsavel> Criar(Responsavel responsavel)
        {
            var communication = new Responsaveis();
            return communication.InserirAsync(responsavel).Result;
        }

        [TestMethod]
        [StatusCodeExpected(HttpStatusCode.Forbidden)]
        public void Obter_Dica_Por_Id_Deslogado()
        {
            Authorization.Singleton.KillToken();
            var communication = new Dicas();
            var result = communication.ObterPorIdAsync(1).Result;
        }
    }
}