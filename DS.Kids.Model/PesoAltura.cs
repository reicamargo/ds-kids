using System;

namespace DS.Kids.Model
{
    /// <summary>
    /// Entidade que representa o lançamento de Peso e Altura
    /// </summary>
    public class PesoAltura : Support.BaseModel
    {
        private int _idCrianca;
        private int _idCrescimento;
        private decimal? _peso;
        private decimal? _altura;

        /// <summary>
        /// Id da Criança
        /// </summary>
        public int IdCrianca
        {
            get { return _idCrianca; }
            set
            {
                _idCrianca = value;
                Notify();
            }
        }

        /// <summary>
        /// Id do Crescimento
        /// </summary>
        public int IdCrescimento
        {
            get { return _idCrescimento; }
            set
            {
                _idCrescimento = value;
                Notify();
            }
        }

        /// <summary>
        /// Peso
        /// </summary>
        public decimal? Peso
        {
            get { return _peso; }
            set
            {
                _peso = value;
                Notify();
            }
        }

        /// <summary>
        /// Altura
        /// </summary>
        public decimal? Altura
        {
            get { return _altura; }
            set
            {
                _altura = value;
                Notify();
            }
        }

        /// <summary>
        /// Atualizar peso, altura do último crescimento
        /// </summary>
        /// <param name="pesoAltura">Informações de Peso e altura</param>
        /// <returns></returns>
        public void AtualizarPesoAlturaUltimoCrescimento(Model.Crescimento ultimoCrescimento)
        {
            if (ultimoCrescimento == null)
                return;

            if (!this.Altura.HasValue || !this.Peso.HasValue)
            {
                if (!this.Altura.HasValue)
                    this.Altura = ultimoCrescimento.Altura;
                if (!this.Peso.HasValue)
                    this.Peso = ultimoCrescimento.Peso;
            }
        }

        /// <summary>
        /// Cálculo do Imc
        /// </summary>
        /// <param name="peso">Peso</param>
        /// <param name="altura">Altura</param>
        /// <returns>Imc</returns>
        public static decimal ObterImc(decimal peso, decimal altura)
        {
            if (altura == 0) return 0;

            var imc = peso / (altura * altura);
            return Math.Truncate(imc);
        }

        /// <summary>
        /// Obtém o Imc
        /// </summary>
        /// <returns>Imc</returns>
        public decimal ObterImc()
        {
            return ObterImc(this.Peso.Value, this.Altura.Value);
        }

        /// <summary>
        /// Valida as informações de Peso e Altura
        /// </summary>
        /// <returns></returns>
        public override Validations.ResultCodes Validate()
        {
            if (this.Peso.HasValue)
            {
                var peso = Validations.Validate.Peso(this.Peso.Value);
                if (peso != Validations.ResultCodes.Success) return peso;
            }

            if (this.Altura.HasValue)
            {
                var altura = Validations.Validate.Altura(this.Altura.Value);
                if (altura != Validations.ResultCodes.Success) return altura;
            }

            if(!this.Peso.HasValue && !this.Altura.HasValue)
                return Validations.ResultCodes.PesoAlturaInvalidos;

            return Validations.ResultCodes.Success;            
        }
    }
}