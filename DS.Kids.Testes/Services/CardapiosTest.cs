using DS.Kids.Model.Repositories;
using DS.Kids.Model.Services;
using DS.Kids.Model.Validations;
using DS.Kids.Testes.Services.__Fakes.Repositories;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DS.Kids.Testes.Services
{
    [TestClass]
    public class CardapiosTest
    {
        private IRefeicoes _refeicoesFake;
        private Cardapio _service;
        private IParceiro _parceiro;

        [TestInitialize]
        public void Initialize()
        {
            Kids.Model.ParceiroSingleton.Instance.Inserir(7);

            var database = new Database();
            _parceiro = Kids.Model.ParceiroSingleton.Instance;
            _refeicoesFake = new RefeicoesFake(database);
            _service = new Cardapio(_refeicoesFake, _parceiro);
        }

        [TestMethod]
        public void Obter_Cardapio_Com_Meses_De_Idade_Inválido()
        {
            var expected = ResultCodes.DataNascimentoInvalidaCrianca;
            var result = _service.ObterAsync(1).Result;
            Assert.AreEqual(expected, result.ResultCode);
        }

        [TestMethod]
        public void Obter_Cardapio_Com_Informacoes_Validas()
        {
            var result = _service.ObterAsync(28).Result;
            Assert.IsNotNull(result);
        }
    }
}
