using System.Collections.Generic;
using System.Threading.Tasks;

namespace DS.Kids.Model.Services
{
    /// <summary>
    /// Serviço de busca de cardápios
    /// </summary>
    public class Cardapio : ICardapio
    {
        private readonly Repositories.IRefeicoes _refeicoes;
        private readonly Repositories.IParceiro _parceiro;
        /// <summary>
        /// Construtor Padrão
        /// </summary>
        /// <param name="refeicoes">Repositório de refeições</param>
        public Cardapio(Repositories.IRefeicoes refeicoes, Repositories.IParceiro parceiro)
        {
            this._refeicoes = refeicoes;
            this._parceiro = parceiro;
        }

        /// <summary>
        /// Obter cardápio
        /// </summary>
        /// <param name="mesesIdade">Meses de Idade para o Cardápio</param>
        /// <returns>Cardápio</returns>
        public async Task<Result<Model.Cardapio>> ObterAsync(int mesesIdade)
        {
            var validate = Validations.Validate.MesesIdadeCrianca(mesesIdade);
            if (validate != Validations.ResultCodes.Success) return new Result<Model.Cardapio>(null, validate);

            var cardapio = new Model.Cardapio();
            cardapio.CafeDaManha = await this._refeicoes.ListarPorMesesDeIdadeTipoRefeicaoAsync(mesesIdade, TipoRefeicao.CafeDaManha, this._parceiro.Obter());
            cardapio.LancheDaManha = await this._refeicoes.ListarPorMesesDeIdadeTipoRefeicaoAsync(mesesIdade, TipoRefeicao.LancheDaManha, this._parceiro.Obter());
            cardapio.Almoco = await this._refeicoes.ListarPorMesesDeIdadeTipoRefeicaoAsync(mesesIdade, TipoRefeicao.Almoco, this._parceiro.Obter());
            cardapio.LancheDaTarde = await this._refeicoes.ListarPorMesesDeIdadeTipoRefeicaoAsync(mesesIdade, TipoRefeicao.LancheDaTarde, this._parceiro.Obter());
            cardapio.Jantar = await this._refeicoes.ListarPorMesesDeIdadeTipoRefeicaoAsync(mesesIdade, TipoRefeicao.Jantar, this._parceiro.Obter());
            cardapio.LancheNoite = await this._refeicoes.ListarPorMesesDeIdadeTipoRefeicaoAsync(mesesIdade, TipoRefeicao.LancheDaNoite, this._parceiro.Obter());

            return new Result<Model.Cardapio>(cardapio);
        }

        /// <summary>
        /// Substitui uma Refeição
        /// </summary>
        /// <param name="mesesIdade">Meses de Idade para a refeição</param>
        /// <param name="tipoRefeicao">Tipo da Refeição</param>
        /// <returns>Itens da Refeição</returns>
        public async Task<Result<Refeicao>> SubstituirRefeicaoAsync(int mesesIdade, TipoRefeicao tipoRefeicao)
        {
            var validate = Validations.Validate.MesesIdadeCrianca(mesesIdade);
            if (validate != Validations.ResultCodes.Success) return new Result<Refeicao>(null, validate);

            var refeicao = await this._refeicoes.ListarPorMesesDeIdadeTipoRefeicaoAsync(mesesIdade, tipoRefeicao, this._parceiro.Obter());

            return new Result<Refeicao>(refeicao);
        }
    }
}
