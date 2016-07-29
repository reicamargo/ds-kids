using System;

using DS.Kids.Model.Support;

namespace DS.Kids.Model
{
    /// <summary>
    /// Entidade que representa o crescimento de uma Crian�a.
    /// Cada vez que o Peso e a altura s�o informados o IMC � calculado.
    /// A partir do Imc, o tipo de crescimento � obtido. Esse tipo indica o qu�o saud�vel est� o crescimento da crian�a
    /// </summary>
    public class Crescimento : Support.BaseModel
    {
        /// <summary>
        /// Id do Crescimento
        /// </summary>
        public int IdCrescimento { get; set; }
        /// <summary>
        /// Id da Crian�a
        /// </summary>
        public int IdCrianca { get; set; }
        /// <summary>
        /// Crian�a
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
        /// Data da Cria��o
        /// </summary>
        public DateTime DataCriacao { get; set; }
        /// <summary>
        /// Data da �ltima atualiza��o
        /// </summary>
        public DateTime DataAtualizacao { get; set; }

        /// <summary>
        /// Construtor Padr�o
        /// </summary>
        public Crescimento()
        {
            this.DataCriacao = this.DataAtualizacao = DateTime.Now;
        }

        /// <summary>
        /// Cria um crescimento para crian�a a partir de informa��es j� cadastradas
        /// </summary>
        /// <param name="crianca">Crian�a</param>
        /// <param name="pesoAlturaAtual">Informa��es de Peso e altura Atual</param>
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
        /// Valida informa��es sobre o Crescimento
        /// </summary>
        /// <returns>Resultado da Valida��o</returns>
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