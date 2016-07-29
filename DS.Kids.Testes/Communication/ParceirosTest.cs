using System.Linq;
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
    public class ParceirosTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Authorization.Singleton.KillToken();
        }

        [TestMethod]
        public void Listar()
        {
            var expected = ResultCodes.Success;

            var responsavel = new Responsavel
            {
                Email = Util.CreateEmail(),
                Nome = Util.CreateString(8),
                Senha = Util.CreateString(6),
                Telefone = "1967739788"
            };
            var responsavelResult = Criar(responsavel);
            Assert.AreEqual(expected, responsavelResult.ResultCode);

            var comm = new Parceiros();
            var result = comm.ListarAsync().Result;
            Authorization.Singleton.KillToken();
            Assert.IsNotNull(result.Data.Count() > 0);
        }

        [TestMethod]
        [StatusCodeExpected(HttpStatusCode.Forbidden)]
        public void Listar_Deslogado()
        {
            var comm = new Parceiros();
            var result = comm.ListarAsync().Result;
        }

        [TestMethod]
        public void Listar_Por_Tipo_Apoio()
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

            var comm = new Parceiros();
            var result = comm.ListarPorTipoAsync(TipoParceiro.Apoio).Result;
            Authorization.Singleton.KillToken();
            Assert.IsNotNull(result.Data.Count() > 0);
        }

        [TestMethod]
        [StatusCodeExpected(HttpStatusCode.Forbidden)]
        public void Listar_Por_Tipo_Apoio_Deslogado()
        {
            var comm = new Parceiros();
            var result = comm.ListarPorTipoAsync(TipoParceiro.Apoio).Result;
        }

        [TestMethod]
        public void Listar_Por_Tipo_Patrocinador()
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

            var comm = new Parceiros();
            var result = comm.ListarPorTipoAsync(TipoParceiro.Patrocinador).Result;
            Authorization.Singleton.KillToken();
            Assert.IsNotNull(result.Data.Count() > 0);
        }

        [TestMethod]
        [StatusCodeExpected(HttpStatusCode.Forbidden)]
        public void Listar_Por_Tipo_Patrocinador_Deslogado()
        {
            var comm = new Parceiros();
            var result = comm.ListarPorTipoAsync(TipoParceiro.Patrocinador).Result;
        }

        private Result<Responsavel> Criar(Responsavel responsavel)
        {
            var communication = new Responsaveis();
            return communication.InserirAsync(responsavel).Result;
        }
    }
}
