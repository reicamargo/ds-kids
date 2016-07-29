using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DS.Kids.Model
{
    /// <summary>
    /// Entidade que representa uma crian�a.
    /// </summary>
    public sealed class Crianca : Support.BaseModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int IdCrianca { get; set; }
        /// <summary>
        /// Id do Respons�vel
        /// </summary>
        public int IdResponsavel { get; set; }
        /// <summary>
        /// Respons�vel
        /// </summary>
        public Responsavel Responsavel { get; set; }
        /// <summary>
        /// Nome
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Imagem zipada
        /// </summary>
        public string ImagemZip { get; set; }
        /// <summary>
        /// Nome da Imagem
        /// </summary>
        public string NomeImagem { get; set; }
        /// <summary>
        /// Array de Bytes que representa uma imagem. Essa propriedade n�o � transitada entre o backend e frontend
        /// </summary>
        [JsonIgnore]
        public byte[] Imagem { get; set; }
        /// <summary>
        /// Url do handler da Imagem da Crian�a
        /// </summary>
        public string UrlImagem
        {
            get
            {
                if (!String.IsNullOrEmpty(this.NomeImagem))
                {
                    var hash = System.IO.Path.GetFileNameWithoutExtension(this.NomeImagem);
                    return string.Format("imagens/criancas/{0}_{1}", this.IdCrianca, hash);
                }
                else
                    return string.Empty;
            }
        }
        /// <summary>
        /// Data de Nascimento
        /// </summary>
        public DateTime DataNascimento { get; set; }
        /// <summary>
        /// Meses de Idade.
        /// O percentil � calculado pelos meses de idade.
        /// </summary>
        public int MesesDeIdade
        {
            get
            {
                return DataNascimento.GetTotalMonths();
            }
        }
        /// <summary>
        /// Sexo da Crian�a. (M|F)
        /// </summary>
        public string Sexo { get; set; }
        /// <summary>
        /// Peso inicial
        /// </summary>
        public decimal PesoInicial { get; set; }
        /// <summary>
        /// Altura Inicial
        /// </summary>
        public decimal AlturaInicial { get; set; }
        /// <summary>
        /// Imc Inicial. Calculado a partir do Peso Inicial e Altura Inicial
        /// </summary>
        public decimal ImcInicial
        {
            get { return Model.PesoAltura.ObterImc(this.PesoInicial, this.PesoInicial); }
        }
        /// <summary>
        /// Data da Cria��o
        /// </summary>
        public DateTime DataCriacao { get; set; }
        /// <summary>
        /// Data da �ltima atualiza��o
        /// </summary>
        public DateTime DataAtualizacao { get; set; }
        /// <summary>
        /// Indica se a Crian�a est� ativa no sistema
        /// </summary>
        public bool Ativo { get; set; }
        /// <summary>
        /// Lista de Informa��es sobre o crescimento
        /// </summary>
        public ICollection<Crescimento> Crescimentos { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<RefeicaoDiario> RefeicoesDiario { get; set; }

        /// <summary>
        /// Construtor Padr�o
        /// </summary>
        public Crianca()
        {
            Crescimentos = new List<Crescimento>();

            this.DataCriacao = DateTime.Now;
            this.DataAtualizacao = DateTime.Now;
            this.Ativo = true;
            this.RefeicoesDiario = new List<RefeicaoDiario>();
        }

        /// <summary>
        /// Valida se uma crian�a pode ser alterada
        /// </summary>
        /// <param name="crianca">Crian�a</param>
        /// <returns>Resultado da Valida��o</returns>
        public Validations.ResultCodes ValidarAtualizacaoCrianca(Model.Crianca crianca)
        {
            Throw.IfIsNull(crianca);

            if (this.Sexo != crianca.Sexo)
                return Validations.ResultCodes.NaoEpossivelAlterarOSexoDaCrianca;

            return Validations.ResultCodes.Success;
        }

        /// <summary>
        /// Valida informa��es sobre a Crian�a
        /// </summary>
        /// <returns>Resultado da Valida��o</returns>
        public override Validations.ResultCodes Validate()
        {
            var nome = Validations.Validate.Nome(this.Nome);
            if (nome != Validations.ResultCodes.Success) return nome;

            var dataNascimento = Validations.Validate.DataNascimentoCrianca(this.DataNascimento);
            if (dataNascimento != Validations.ResultCodes.Success) return dataNascimento;

            var sexo = Validations.Validate.Sexo(this.Sexo);
            if (sexo != Validations.ResultCodes.Success) return sexo;

            var imagem = Validations.Validate.Imagem(this.Imagem, true);
            if (imagem != Validations.ResultCodes.Success) return sexo;

            var pesoInicial = Validations.Validate.Peso(this.PesoInicial);
            if (pesoInicial != Validations.ResultCodes.Success) return pesoInicial;

            var alturaInicial = Validations.Validate.Altura(this.AlturaInicial);
            if (alturaInicial != Validations.ResultCodes.Success) return alturaInicial;

            return Validations.ResultCodes.Success;
        }
    }
}
