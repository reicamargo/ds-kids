using System;

using DS.Kids.Model.Support;

namespace DS.Kids.Model
{
    /// <summary>
    /// Entidade que representa o crescimento de uma Criança.
    /// Cada vez que o Peso e a altura são informados o IMC é calculado.
    /// A partir do Imc, o tipo de crescimento é obtido. Esse tipo indica o quão saudável está o crescimento da criança
    /// </summary>
    public class Crescimento : Support.BaseModel
    {
        /// <summary>
        /// Id do Crescimento
        /// </summary>
        public int IdCrescimento { get; set; }
        /// <summary>
        /// Id da Criança
        /// </summary>
        public int IdCrianca { get; set; }
        /// <summary>
        /// Criança
        /// </summary>
        public virtual Crianca Crianca { get; set; }
        /// <summary>
        /// Peso informado
        /// </summary>
        public decimal Peso { get; set; }
        /// <summary>
        /// Altura
        /// </summary>
        public decimal Altura { get; set; }
        /// <summary>
        /// Meses de Idade
        /// </summary>
        public int MesesDeIdade { get; set; }
        /// <summary>
        /// Tipo de Crescimento
        /// </summary>
        public TipoCrescimento TipoCrescimento { get; set; }
        /// <summary>
        /// Data da Criação
        /// </summary>
        public DateTime DataCriacao { get; set; }
        /// <summary>
        /// Data da última atualização
        /// </summary>
        public DateTime DataAtualizacao { get; set; }

        /// <summary>
        /// Construtor Padrão
        /// </summary>
        public Crescimento()
        {
            this.DataCriacao = this.DataAtualizacao = DateTime.Now;
        }

        /// <summary>
        /// Cria um crescimento para criança a partir de informações já cadastradas
        /// </summary>
        /// <param name="crianca">Criança</param>
        /// <param name="pesoAlturaAtual">Informações de Peso e altura Atual</param>
        public Crescimento(Model.Crianca crianca, Model.PesoAltura pesoAlturaAtual)
        {
            this.DataAtualizacao = this.DataCriacao = DateTime.Now;
            
            var mesesIdade = crianca.DataNascimento.GetTotalMonths();
            var imc = pesoAlturaAtual.ObterImc();

            this.IdCrescimento = pesoAlturaAtual.IdCrescimento;
            this.Altura = pesoAlturaAtual.Altura.Value;
            this.Peso = pesoAlturaAtual.Peso.Value;
            this.IdCrianca = crianca.IdCrianca;
            this.TipoCrescimento = Percentil.ObterTipoCrescimento(crianca.Sexo, mesesIdade, imc);
            this.MesesDeIdade = crianca.DataNascimento.GetTotalMonths();
        }

        /// <summary>
        /// Valida informações sobre o Crescimento
        /// </summary>
        /// <returns>Resultado da Validação</returns>
        public override Validations.ResultCodes Validate()
        {
            var peso = Validations.Validate.Peso(this.Peso);
            if (peso != Validations.ResultCodes.Success) return peso;

            var altura = Validations.Validate.Altura(this.Altura);
            if (altura != Validations.ResultCodes.Success) return altura;

            return Validations.ResultCodes.Success;
        }
    }
}