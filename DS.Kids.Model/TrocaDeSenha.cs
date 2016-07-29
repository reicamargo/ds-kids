namespace DS.Kids.Model
{
    /// <summary>
    /// Entidade que representa uma troca de senha
    /// </summary>
    public class TrocaDeSenha : Support.BaseModel
    {
        /// <summary>
        /// Id do Responsável que terá a senha trocada
        /// </summary>
        public int IdResponsavel { get; set; }

        /// <summary>
        /// Senha Atual
        /// </summary>
        public string SenhaAtual { get; set; }
        /// <summary>
        /// Nova Senha
        /// </summary>
        public string NovaSenha { get; set; }

        /// <summary>
        /// Valida as informações de Troca de Senha
        /// </summary>
        /// <returns>Resultado da Validação</returns>
        public override Validations.ResultCodes Validate()
        {
            var trocaSenha = Validations.Validate.TrocaDeSenha(this.SenhaAtual, this.NovaSenha);
            if (trocaSenha != Validations.ResultCodes.Success) return trocaSenha;

            return Validations.ResultCodes.Success;
        }
    }
}