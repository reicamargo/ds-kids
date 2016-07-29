using System;

using DS.Kids.Model;
using DS.Kids.Model.Repositories;
using DS.Kids.Model.Services;
using DS.Kids.Model.Validations;
using DS.Kids.Testes.Services.__Fakes.Repositories;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Diario = DS.Kids.Model.Services.Diario;

namespace DS.Kids.Testes.Services
{
    [TestClass]
    public class DiarioTest
    {
        private IRefeicoesDiarios _refeicoesDiarios;
        private IRefeicoesGrupos _refeicoesGrupos;
        private ICriancas _criancas;
        private IDiario _service;

        [TestInitialize]
        public void Initialize()
        {
            var database = new Database();
            _refeicoesDiarios = new RefeicoesDiariosFake(database);
            _refeicoesGrupos = new RefeicoesGruposFake(database);
            _criancas = new CriancasFake(database);
            _service = new Diario(_refeicoesDiarios, _refeicoesGrupos, _criancas);
        }

        [TestMethod]
        public void Obter_Diario_Com_Dados_Validos()
        {
            var result = _service.ObterPorIdDataAsync(1, DateTime.Now).Result;
            Assert.AreEqual(ResultCodes.Success, result.ResultCode);
        }

        [TestMethod]
        public void Obter_Diario_Com_Crianca_Invalida()
        {
            var result = _service.ObterPorIdDataAsync(150, DateTime.Now).Result;
            Assert.AreEqual(ResultCodes.CriancaNaoEncontrada, result.ResultCode);
        }

        [TestMethod]
        public void Obter_Diario_Com_Data_Nascimento_Invalida()
        {
            //Data anterior aos 2 anos da criança
            var result = _service.ObterPorIdDataAsync(1, DateTime.Now.AddYears(-3)).Result;
            Assert.AreEqual(ResultCodes.DataNascimentoInvalidaCrianca, result.ResultCode);

            //Data posterior aos 10 anos da criança
            result = _service.ObterPorIdDataAsync(1, DateTime.Now.AddYears(-3)).Result;
            Assert.AreEqual(ResultCodes.DataNascimentoInvalidaCrianca, result.ResultCode);
        }

        [TestMethod]
        public void Obter_Diario_Com_Data_Futura()
        {
            var result = _service.ObterPorIdDataAsync(1, DateTime.Now.AddDays(1)).Result;
            Assert.AreEqual(ResultCodes.DataInvalida, result.ResultCode);
        }

        [TestMethod]
        public void Diario_Deve_Possuir_Seis_Refeicoes()
        {
            var result = _service.ObterPorIdDataAsync(1, DateTime.Now).Result;
            Assert.AreEqual(ResultCodes.Success, result.ResultCode);
        }

        [TestMethod]
        public void Atualizar_Diario_Check()
        {
            var diarioDTO = new DiarioDTO
            {
                Data = DateTime.Now,
                IdCrianca = 1,
                IdGrupo = TipoGrupoRefeicao.VerdurasELegumes,
                IdTipoRefeicao = TipoRefeicao.Jantar
            };

            var result = _service.AtualizarAsync(diarioDTO).Result;
            Assert.AreEqual(ResultCodes.Success, result.ResultCode);
        }

        [TestMethod]
        public void Atualizar_Diario_Remover_Check()
        {
            var diarioDTO = new DiarioDTO
            {
                Data = DateTime.Now,
                IdCrianca = 2,
                IdGrupo = TipoGrupoRefeicao.Frutas,
                IdTipoRefeicao = TipoRefeicao.CafeDaManha,
                Checked = true
            };

            var resultChecked = _service.AtualizarAsync(diarioDTO).Result;

            Assert.AreEqual(ResultCodes.Success, resultChecked.ResultCode);
            Assert.IsNotNull(resultChecked.Data.IdRefeicao);
            Assert.IsNotNull(resultChecked.Data.IdRefeicaoGrupo);

            diarioDTO.Checked = false;

            var result = _service.AtualizarAsync(diarioDTO).Result;

            Assert.AreEqual(ResultCodes.Success, result.ResultCode); 
            Assert.IsNull(resultChecked.Data.IdRefeicaoGrupo);
        }

        [TestMethod]
        public void Atualizar_Diario_Alimento()
        {
            var diarioDTO = new DiarioDTO
            {
                Data = DateTime.Now,
                IdCrianca = 1,
                IdGrupo = TipoGrupoRefeicao.CereaisTuberculosERaizes,
                IdTipoRefeicao = TipoRefeicao.Jantar,
                IdAlimento = 1
            };

            var result = _service.AtualizarAsync(diarioDTO).Result;
            Assert.AreEqual(ResultCodes.Success, result.ResultCode);
        }

        [TestMethod]
        public void Atualizar_Diario_Adicionar_Duas_Vezes_O_Mesmo_Alimento_Adiciona_Apenas_Uma_Vez()
        {
            var diarioDTO = new DiarioDTO
            {
                Data = DateTime.Now,
                IdCrianca = 1,
                IdGrupo = TipoGrupoRefeicao.CereaisTuberculosERaizes,
                IdTipoRefeicao = TipoRefeicao.Jantar,
                IdAlimento = 1
            };

            var result = _service.AtualizarAsync(diarioDTO).Result;
            Assert.AreEqual(ResultCodes.Success, result.ResultCode);
            var result2 = _service.AtualizarAsync(diarioDTO).Result;
            Assert.AreEqual(ResultCodes.AlimentosRefeicaoDuplicado, result2.ResultCode);
        }

        [TestMethod]
        public void Atualizar_Diario_Adicionar_Tres_Alimentos_Diferentes()
        {
            var diarioDTO = new DiarioDTO
            {
                Data = DateTime.Now,
                IdCrianca = 1,
                IdGrupo = TipoGrupoRefeicao.CereaisTuberculosERaizes,
                IdTipoRefeicao = TipoRefeicao.Jantar,
                IdAlimento = 1
            };

            var result = _service.AtualizarAsync(diarioDTO).Result;
            Assert.AreEqual(ResultCodes.Success, result.ResultCode);
            diarioDTO.IdAlimento = 2;
            var result2 = _service.AtualizarAsync(diarioDTO).Result;
            Assert.AreEqual(ResultCodes.Success, result2.ResultCode);
            diarioDTO.IdAlimento = 3;
            var result3 = _service.AtualizarAsync(diarioDTO).Result;
            Assert.AreEqual(ResultCodes.Success, result3.ResultCode);
        }

        [TestMethod]
        public void Remover_Alimento_Diario()
        {
            var diarioDTO = new DiarioDTO
            {
                Data = DateTime.Now,
                IdCrianca = 1,
                IdGrupo = TipoGrupoRefeicao.VerdurasELegumes,
                IdTipoRefeicao = TipoRefeicao.CafeDaManha,
                IdAlimento = 1
            };

            var result = _service.AtualizarAsync(diarioDTO).Result;
            Assert.AreEqual(ResultCodes.Success, result.ResultCode);

            Assert.IsNotNull(result.Data.IdRefeicaoGrupo);

            result = _service.RemoverAlimentoAsync(result.Data.IdRefeicaoGrupo.Value, 1).Result;
            Assert.AreEqual(ResultCodes.Success, result.ResultCode);
        }
    }
}
