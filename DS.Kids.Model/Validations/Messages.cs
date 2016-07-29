using System.Collections.Generic;

namespace DS.Kids.Model.Validations
{
	/// <summary>
	/// Classe para gerenciamento de Mensagens
	/// </summary>
	public static class Messages
	{
		/// <summary>
		/// Dicionário em cache das mensagens
		/// </summary>
		private static readonly Dictionary<ResultCodes, string> _messages;

		/// <summary>
		/// Construtor padrão. 
		/// As mensagens são carregadas uma única vez aqui
		/// </summary>
		static Messages()
		{
			IMessageResource resource = GetResources();
			_messages = resource.Get();
		}

		public static IMessageResource GetResources()
		{
			// TODO: Avaliar como deixar flexível para globalização e localização do App
			return new PtBr();
		}

		/// <summary>
		/// Obtém uma mensagem cacheada a partir de um código de resultado de Operação
		/// </summary>
		/// <param name="resultCodes">código de resultado de Operação</param>
		/// <returns>Mensagem</returns>
		public static string Get(ResultCodes resultCodes)
		{
			return _messages[resultCodes];
		}
	}


	/*
	 * Essa é uma ideia para poder deixar o app utilizando resources de tradução.
	 * Em cada app, o arquivo de resource é criado com as traduções, respeitando o enum de mesnsagens.
	 * Essas traduções deverão ser injetadas na classe de mensagem que se encarrega de gerenciar os textos que serão mostrados
	 */
	public interface IMessageResource
	{
		Dictionary<ResultCodes, string> Get();
	}

	/// <summary>
	/// Entidade que representa mensagems em Português.
	/// </summary>
	internal class PtBr : IMessageResource
	{
		/// <summary>
		/// Retorna o dicionário de mensagens em português
		/// </summary>
		/// <returns></returns>
		public Dictionary<ResultCodes, string> Get()
		{
			var messages = new Dictionary<ResultCodes, string>
					{
						{ ResultCodes.Success, "Sucesso" },
						{ ResultCodes.ErroDesconhecido, "Erro Desconhecido" },
						{ ResultCodes.ResponsavelNaoEncontrado, "Responsável não encontrado" },
						{ ResultCodes.LoginOuSenhaInvalidos, "Login ou Senha inválidos" },
						{ ResultCodes.CriancaNaoEncontrada, "Criança não encontrada" },
						{ ResultCodes.CriancaJaCadastrada, "Criança já adicionada" },
						{ ResultCodes.NomeObrigatorio, "Nome é obrigatório" },
						{ ResultCodes.TamanhoMaximoCampoNome, "O campo nome deve ter até 50 caracteres" },
						{ ResultCodes.EmailObrigatorio, "Email é obrigatório" },
						{ ResultCodes.TamanhoMaximoCampoEmail, "Email deve ter menos que 80 caracteres" },
						{ ResultCodes.EmailInvalido, "Email inválido" },
						{ ResultCodes.SenhaObrigatoria, "Senha é obrigatória" },
						{ ResultCodes.TamanhoMaximoCampoSenha, "Senha deve ter menos que 20 caracteres" },
						{ ResultCodes.TamanhoMinimoCampoSenha, "Senha deve ter mais que 5 caracteres" },
						{ ResultCodes.TelefoneObrigatorio, "Telefone é obrigatório" },
						{ ResultCodes.TelefoneInvalido, "Telefone inválido" },
						{ ResultCodes.SexoObrigatorio, "Sexo é obrigatório" },
						{ ResultCodes.SexoInvalido, "Sexo inválido. Permitido F/M" },
						{ ResultCodes.DataNascimentoInvalidaCrianca, "A criança deve ter de 2 a 10 anos" },
						{ ResultCodes.ImcInvalido, "IMC inválido" },
						{ ResultCodes.PesoInvalido, "Peso inválido" },
						{ ResultCodes.AlturaInvalida, "Altura inválida" },
						{ ResultCodes.PesoAlturaInvalidos, "Peso ou Altura inválidos" },
						{ ResultCodes.SenhaAtualInvalida, "Senha Atual inválida" },
						{ ResultCodes.NovaSenhaInvalida, "Nova Senha inválida" },
						{ ResultCodes.ConfirmacaoSenhaInvalida, "Confirmação de Senha inválida" },
						{ ResultCodes.NovaSenhaIgualSenhaAtual, "Nova Senha igual Senha Atual" },
						{ ResultCodes.ChaveRedeSocialObrigatoria, "Chave da Rede social inválida" },
						{ ResultCodes.TokenRecuperacaoSenhaInvalido, "Token de Recuperação de Senha Inválido" },
						{ ResultCodes.EmailResponsavelJaCadastradoNoSistema, "Email do Responsável já Cadastrado no Sistema" },
						{ ResultCodes.NaoEpossivelAlterarOSexoDaCrianca, "Não é possível alterar o sexo da criança" },
						{ ResultCodes.Opsss, "Oppsssss" },
						{ ResultCodes.AcessoNegado, "Acesso Negado" },
						{ ResultCodes.Timeout, "Houve um erro ao conectar a internet. Por favor, tente novamente!!" },
						{ ResultCodes.ImagemObrigatoria, "Imagem Obrigatória" },
						{ ResultCodes.TamanhoImagemInvalido, "Tamanho da Imagem inválido" },
						{ ResultCodes.RefeicaoDuplicada, "Já existe uma refeição deste tipo cadastrada nesta data para esta criança" },
						{ ResultCodes.RefeicaoNaoEncontrada, "Refeição não encontrada." },
						{ ResultCodes.AlimentosRefeicaoDuplicado, "Este alimento já esta adicionado nesta refeição." },
						{ ResultCodes.DataInvalida, "A data selecionada não pode ser uma data futura." }
					};

			return messages;
		}
	}
}
