using System.Linq;

using DS.Kids.Model;
using DS.Kids.Model.Repositories;
using DS.Kids.Model.Services;
using DS.Kids.Model.Validations;
using DS.Kids.Testes.Services.__Fakes.Repositories;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Alimento = DS.Kids.Model.Services.Alimento;

namespace DS.Kids.Testes.Services
{
    [TestClass]
    public class AlimentoTest
    {
        private IAlimentos _alimentos;

        private IAlimento _service;

        [TestInitialize]
        public void Initialize()
        {
            var database = new Database();
            _alimentos = new AlimentosFake(database);
            _service = new Alimento(_alimentos);
        }

        [TestMethod]
        public void Obter_Alimentos_Valido()
        {
            var expected = ResultCodes.Success;

            for(int i = 24; i < 11 * 12; i++)
            {
                var alimentosResult = _service.ObterPorGrupoAlimentar(i, (int)TipoGrupoRefeicao.CereaisTuberculosERaizes).Result;
                Assert.AreEqual(expected, alimentosResult.ResultCode);
                Assert.IsNotNull(alimentosResult.Data);
            }
        }

        [TestMethod]
        public void Obter_Alimentos_Id_Invalido()
        {
            var mesesDeIdade = 36;
            var idGrupoRefeicao = 10;

            var alimentosResult = _service.ObterPorGrupoAlimentar(mesesDeIdade, idGrupoRefeicao).Result;

            Assert.AreEqual(ResultCodes.Success, alimentosResult.ResultCode);
            Assert.IsNotNull(alimentosResult.Data);
            Assert.IsFalse(alimentosResult.Data.Any());
        }
    }
}
