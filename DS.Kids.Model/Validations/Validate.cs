using System;
using System.Text.RegularExpressions;

namespace DS.Kids.Model.Validations
{
    /// <summary>
    /// Classe com validações dos Modelos
    /// </summary>
    public static class Validate
    {
        /// <summary>
        /// Validação da Propriedade Nome
        /// </summary>
        /// <param name="nome">Nome</param>
        /// <param name="obrigatorio">Indica se esse campo é obrigatório ou não</param>
        /// <returns>Um resultado de validação. Validações que podem ser retornadas: ResultCodes.NomeObrigatorio, ResultCodes.TamanhoMaximoCampoNome e Result.SUCCESS</returns>
        public static Validations.ResultCodes Nome(string nome, bool obrigatorio = true)
        {
            if (obrigatorio)
                if (string.IsNullOrEmpty(nome))
                    return ResultCodes.NomeObrigatorio;

            if (nome.Length > 50)
                return ResultCodes.TamanhoMaximoCampoNome;

            return ResultCodes.Success;
        }

        /// <summary>
        /// Validação da Propriedade Email
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="obrigatorio">Indica se esse campo é obrigatório ou não</param>
        /// <returns>Um resultado de validação. Validações que podem ser retornadas: ResultCodes.EmailObrigatorio, ResultCodes.TamanhoMaximoCampoEmail, ResultCodes.EmailInvalido,  e Result.SUCCESS</returns>
        public static Validations.ResultCodes Email(string email, bool obrigatorio = true)
        {
            if (obrigatorio)
                if (string.IsNullOrEmpty(email))
                    return ResultCodes.EmailObrigatorio;

            if (email.Length > 80)
                return ResultCodes.TamanhoMaximoCampoEmail;

            string pattern = @"^([a-zA-Z0-9_\-\.\+]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            if (!Regex.IsMatch(email, pattern))
                return ResultCodes.EmailInvalido;

            return ResultCodes.Success;
        }

        /// <summary>
        /// Validação da Propriedade Senha
        /// </summary>
        /// <param name="senha">Senha</param>
        /// <returns>Um resultado de validação. Validações que podem ser retornadas: ResultCodes.SenhaObrigatoria, ResultCodes.TamanhoMaximoCampoSenha,  e Result.SUCCESS</returns>
        public static Validations.ResultCodes Senha(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
                return ResultCodes.SenhaObrigatoria;

            if (senha.Length > 20)
                return ResultCodes.TamanhoMaximoCampoSenha;

            if (senha.Length < 6)
                return ResultCodes.TamanhoMinimoCampoSenha;

            return ResultCodes.Success;
        }

        /// <summary>
        /// Validação da Confirmacao de Senha
        /// </summary>
        /// <param name="novaSenha">Nova Senha</param>
        /// <param name="confirmacaoSenha">Confirmacao de Senha</param>
        /// <returns>Um resultado de validação. Validações que podem ser retornadas: ResultCodes.SenhaAtualInvalida, ResultCodes.NovaSenhaInvalida, ResultCodes.ConfirmacaoSenhaInvalida,  e Result.SUCCESS</returns>
        public static Validations.ResultCodes ConfirmacaoSenha(string novaSenha, string confirmacaoSenha)
        {
            var novaSenhaValidacao = Validate.Senha(novaSenha);
            if (novaSenhaValidacao != ResultCodes.Success) return ResultCodes.NovaSenhaInvalida;

            var confirmacaoSenhaValidacao = Validate.Senha(confirmacaoSenha);
            if (confirmacaoSenhaValidacao != ResultCodes.Success) return ResultCodes.ConfirmacaoSenhaInvalida;

            if (novaSenha.Equals(confirmacaoSenha))
                return ResultCodes.NovaSenhaIgualSenhaAtual;

            return ResultCodes.Success;
        }

        /// <summary>
        /// Validação de Troca de Senha
        /// </summary>
        /// <param name="novaSenha">Nova Senha</param>
        /// <param name="trocaSenha">Troca de Senha</param>
        /// <returns>Um resultado de validação. Validações que podem ser retornadas: ResultCodes.SenhaAtualInvalida, ResultCodes.NovaSenhaInvalida, ResultCodes.NovaSenhaIgualSenhaAtual,  e Result.SUCCESS</returns>
        public static Validations.ResultCodes TrocaDeSenha(string novaSenha, string trocaSenha)
        {
            var novaSenhaValidacao = Validations.Validate.Senha(novaSenha);
            if (novaSenhaValidacao != ResultCodes.Success) return ResultCodes.SenhaAtualInvalida;

            var trocaSenhaValidacao = Validations.Validate.Senha(trocaSenha);
            if (trocaSenhaValidacao != ResultCodes.Success) return ResultCodes.NovaSenhaInvalida;

            if (novaSenha.Equals(trocaSenha))
                return ResultCodes.NovaSenhaIgualSenhaAtual;

            return ResultCodes.Success;
        }

        /// <summary>
        /// Validação de Troca de Senha
        /// </summary>
        /// <param name="novaSenha">Nova Senha</param>
        /// <param name="confirmacaoSenha">Troca de Senha</param>
        /// <returns>Um resultado de validação. Validações que podem ser retornadas: ResultCodes.SenhaAtualInvalida, ResultCodes.NovaSenhaInvalida, ResultCodes.NovaSenhaIgualSenhaAtual,  e Result.SUCCESS</returns>
        public static Validations.ResultCodes EsqueciMinhaSenha(string novaSenha, string confirmacaoSenha)
        {
            var novaSenhaValidacao = Validations.Validate.Senha(novaSenha);
            if (novaSenhaValidacao != ResultCodes.Success) return ResultCodes.SenhaAtualInvalida;

            var trocaSenhaValidacao = Validations.Validate.Senha(confirmacaoSenha);
            if (trocaSenhaValidacao != ResultCodes.Success) return ResultCodes.NovaSenhaInvalida;

            if (!novaSenha.Equals(confirmacaoSenha))
                return ResultCodes.ConfirmacaoSenhaInvalida;

            return ResultCodes.Success;
        }

        /// <summary>
        /// Validação da Propriedade Telefone
        /// </summary>
        /// <param name="telefone">Telefone</param>
        /// <param name="obrigatorio">Indica se esse campo é obrigatório ou não</param>
        /// <returns>Um resultado de validação. Validações que podem ser retornadas: ResultCodes.TelefoneObrigatorio e Result.SUCCESS</returns>
        public static Validations.ResultCodes Telefone(string telefone, bool obrigatorio = true)
        {
            if (obrigatorio)
                if (string.IsNullOrEmpty(telefone))
                    return ResultCodes.TelefoneObrigatorio;

            if (string.IsNullOrEmpty(telefone))
                return ResultCodes.Success;

            if (telefone.Length > 11)
                return ResultCodes.TelefoneInvalido;

            var count = 0;
            var aux = telefone[0];
            for (var y = 0; y < telefone.Length; y++)
                if (aux == telefone[y])
                    count++;

            if (count == telefone.Length)
                return ResultCodes.TelefoneInvalido;

            return ResultCodes.Success;
        }

        /// <summary>
        /// Validação da Propriedade Sexo
        /// </summary>
        /// <param name="sexo">Sexo</param>
        /// <param name="obrigatorio">Indica se esse campo é obrigatório ou não</param>
        /// <returns>Um resultado de validação. Validações que podem ser retornadas: ResultCodes.TelefoneObrigatorio e Result.SUCCESS</returns>
        public static Validations.ResultCodes Sexo(string sexo, bool obrigatorio = true)
        {
            if (obrigatorio)
                if (string.IsNullOrEmpty(sexo))
                    return ResultCodes.SexoObrigatorio;

            if (sexo != "F" && sexo != "M")
                return ResultCodes.SexoInvalido;

            return ResultCodes.Success;
        }

        /// <summary>
        /// Validação da Propriedade Data de Nascimento
        /// </summary>
        /// <param name="dataNascimento">Data de Nascimento</param>
        /// <returns>Um resultado de validação. Validações que podem ser retornadas: ResultCodes.DatanascimentoInvalidoCrianca e Result.SUCCESS</returns>
        public static Validations.ResultCodes DataNascimentoCrianca(DateTime dataNascimento)
        {
            var idade = dataNascimento.Age();
            if (idade < 2 || idade > 10)
                return ResultCodes.DataNascimentoInvalidaCrianca;

            return ResultCodes.Success;
        }

        /// <summary>
        /// Validação da Propriedade Meses de Idade
        /// </summary>
        /// <param name="mesesIdade">Meses de Idade</param>
        /// <returns>Um resultado de validação. Validações que podem ser retornadas: ResultCodes.DataNascimentoInvalidoCrianca e Result.SUCCESS</returns>
        public static Validations.ResultCodes MesesIdadeCrianca(decimal mesesIdade)
        {
            if (mesesIdade < 24 || mesesIdade > 144)
                return ResultCodes.DataNascimentoInvalidaCrianca;

            return ResultCodes.Success;
        }

        /// <summary>
        /// Validação da Propriedade IMC
        /// </summary>
        /// <param name="imc">Imc</param>
        /// <returns>Um resultado de validação. Validações que podem ser retornadas: ResultCodes.DataNascimentoInvalidoCrianca e Result.SUCCESS</returns>
        public static Validations.ResultCodes IMC(decimal imc)
        {
            if (imc < 3.2m || imc > 30)
                return ResultCodes.ImcInvalido;

            return ResultCodes.Success;
        }

        /// <summary>
        /// Validação da Propriedade Peso
        /// </summary>
        /// <param name="peso">Peso</param>
        /// <returns>Um resultado de validação. Validações que podem ser retornadas: ResultCodes.DataNascimentoInvalidoCrianca e Result.SUCCESS</returns>
        public static Validations.ResultCodes Peso(decimal peso)
        {
            if (peso < 5 || peso > 120)
                return ResultCodes.PesoInvalido;

            return ResultCodes.Success;
        }

        /// <summary>
        /// Validação da Propriedade Altura
        /// </summary>
        /// <param name="altura">Altura</param>
        /// <returns>Um resultado de validação. Validações que podem ser retornadas: ResultCodes.DataNascimentoInvalidoCrianca e Result.SUCCESS</returns>
        public static Validations.ResultCodes Altura(decimal altura)
        {
            if (altura < 0.80m || altura > 2.00m)
                return ResultCodes.AlturaInvalida;

            return ResultCodes.Success;
        }

        /// <summary>
        /// Validação da Propriedade Chave da Rede Social
        /// </summary>
        /// <param name="chave">Senha</param>
        /// <returns>Um resultado de validação. Validações que podem ser retornadas: ResultCodes.ChaveRedeSocialObrigatoria e Result.SUCCESS</returns>
        public static Validations.ResultCodes ChaveRedeSocial(string chave)
        {
            if (string.IsNullOrWhiteSpace(chave))
                return ResultCodes.ChaveRedeSocialObrigatoria;

            return ResultCodes.Success;
        }

        /// <summary>
        /// Valida a propriedade Token de Recuperação de Senha
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns>Um resultado de Validação. Validações que podem ser retornadas: ResultCodes.TokenRecuperacaoSenhaInvalido e ResultCodes.Success</returns>
        public static Validations.ResultCodes TokenRecuperacaoSenha(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return ResultCodes.TokenRecuperacaoSenhaInvalido;

            return ResultCodes.Success;
        }

        /// <summary>
        /// Valida a propriedade Imagem
        /// </summary>
        /// <param name="imagem">Imagem em bytes</param>
        /// <returns>Um resultado de Validação. Validações que podem ser retornadas: ResultCodes.ImagemObrigatoria, ResultCodes.TamanhoImagemInvalido e ResultCodes.Success</returns>
        public static Validations.ResultCodes Imagem(byte[] imagem, bool obrigatorio = false)
        {
            if (imagem != null && imagem.Length == 0 && obrigatorio)
                return ResultCodes.ImagemObrigatoria;

            /*Definição da regra do tamanho da imagem*/
            //var megabyte = imagem.Length / 1024f;
            //if (megabyte > 12312)
            //    return ResultCodes.TamanhoImagemInvalido;

            return ResultCodes.Success;
        }
    }
}
