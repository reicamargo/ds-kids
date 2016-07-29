using System;
using System.Threading.Tasks;

namespace DS.Kids.Model.Services
{
    /// <summary>
    /// Serviço de Crescimento
    /// </summary>
    public class Crescimento : ICrescimento
    {
        private readonly Repositories.ICriancas _criancas;
        private readonly Repositories.ICrescimentos _crescimentos;
        /// <summary>
        /// Construtor padrão
        /// </summary>
        /// <param name="criancas">Repositório de crianças</param>
        /// <param name="crescimentos">Repositório de crescimento</param>
        public Crescimento(Repositories.ICriancas criancas, Repositories.ICrescimentos crescimentos)
        {
            Throw.IfIsNull(criancas);
            Throw.IfIsNull(crescimentos);

            this._criancas = criancas;
            this._crescimentos = crescimentos;
        }

        /// <summary>
        /// Inserir crescimento
        /// </summary>
        /// <param name="pesoAltura">Informações de Peso e Altura</param>
        /// <returns>Retorna o tipo de crescimento atingido</returns>
        public async Task<Model.Result<Model.Crescimento>> InserirAsync(Model.PesoAltura pesoAltura)
        {
            var result = await this.ObterCrescimentoAsync(pesoAltura);
            if (result.ResultCode != Validations.ResultCodes.Success) return result;

            await this._crescimentos.InserirAsync(result.Data);
            return new Model.Result<Model.Crescimento>(result.Data);
        }

        /// <summary>
        /// Atualiza uma informação de Peso e Altura da Criança
        /// </summary>
        /// <param name="pesoAltura">Informação de Peso e Altura</param>
        /// <returns>Crescimento</returns>
        public async Task<Result<Model.Crescimento>> AtualizarAsync(PesoAltura pesoAltura)
        {
            var result = await this.ObterCrescimentoAsync(pesoAltura);
            if (result.ResultCode != Validations.ResultCodes.Success) return result;

            await this._crescimentos.AtualizarAsync(result.Data);
            return new Model.Result<Model.Crescimento>(result.Data);
        }

        /// <summary>
        /// Obtém um crescimento a partir da informação de Peso e Altura. 
        /// Esse crescimento pode ser um novo, ou uma atualização, dependendo da informação de Peso e Altura
        /// </summary>
        /// <param name="pesoAltura">Informações sobre Peso e Altura</param>
        /// <returns>Crescimento</returns>
        private async Task<Model.Result<Model.Crescimento>> ObterCrescimentoAsync(PesoAltura pesoAltura)
        {
            Throw.IfIsNull(pesoAltura);

            var validate = pesoAltura.Validate();
            if (validate != Validations.ResultCodes.Success) return new Model.Result<Model.Crescimento>(validate);

            var crianca = await this._criancas.ObterPorIdAsync(pesoAltura.IdCrianca);
            if (crianca == null)
                return new Model.Result<Model.Crescimento>(null, Validations.ResultCodes.CriancaNaoEncontrada);

            var crescimento = new Model.Crescimento(crianca, pesoAltura);

            return new Model.Result<Model.Crescimento>(crescimento);
        }
    }
}
