using BRFX.Core;
using BRFX.Core.ViewModels;

using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;

using DS.Kids.Apps.Core.Analytics;
using DS.Kids.Apps.Core.Helpers;
using DS.Kids.Apps.Core.Messages;
using DS.Kids.Apps.Core.Resources;
using DS.Kids.Model;

namespace DS.Kids.Apps.Core.ViewModels
{

	public class AvaliacaoViewModel : BaseViewModel<AvaliacaoViewModelParams>
	{

		private string _descricao;

		private bool _modal;

		public AvaliacaoViewModel()
		{
			var analytics = Mvx.Resolve<IAnalytics>();
			analytics.SendView("AvaliacaoView");
		}

		public string Descricao
		{
			get
			{
				return _descricao;
			}
			set
			{
				Set(ref _descricao, value);
			}
		}

		public MvxCommand ComecarCommand { get; private set; }

		protected override void CreateCommands()
		{
			base.CreateCommands();

			ComecarCommand = new MvxCommand(ExecuteComecarCommand);
		}

		private void ExecuteComecarCommand()
		{
			if(_modal)
			{
				Close(this);
			}
			else
			{
				if(PlatformInstance.Platform == Platform.iOS)
				{
					NavigateTo<HomeViewModel>();
				}
				else
				{
					Messenger.Publish(new ClearBackStackMessage(this));
					NavigateTo<DiarioViewModel>();
				}
			}
		}

		public override string GetResourceStringForIndex(string index)
		{
			return AppResources.ResourceManager.GetString(index);
		}

		/// <summary>
		///     Obtém os parâmetros enviado para este view model.
		/// </summary>
		protected override void GetParams(AvaliacaoViewModelParams e)
		{
			_modal = e.Modal;

			string mensagem;
			switch(e.TipoCrescimento)
			{
				case TipoCrescimento.Desnutricao:
					mensagem = "{NOMERESPONSAVEL}, analisando os dados que você nos informou para a avaliação d{SEXO} {NOMECRIANCA} observamos que {ELEELA} está abaixo do peso saudável! Continue usando as nossas dicas, mas nesse caso, não deixe de leva-lo às consultas de rotina com o pediatra para que, juntos, possam investigar as razões do baixo peso.";
					break;
				case TipoCrescimento.Normal:
					mensagem = "Parabéns {NOMERESPONSAVEL}, analisando os dados que você nos informou para a avaliação d{SEXO} {NOMECRIANCA} observamos que {ELEELA} está dentro do peso saudável! Para ter dicas sobre alimentação infantil e tornar o estilo de vida d{SEXO} {NOMECRIANCA} mais saudável, não deixe de usar o DS Kids.";
					break;
				case TipoCrescimento.Sobrepeso:
					mensagem = "{NOMERESPONSAVEL}, analisando os dados que você nos informou para a avaliação d{SEXO} {NOMECRIANCA} observamos que {ELEELA} está em sobrepeso. Com a ajuda do DS Kids e do pediatra, você conseguirá identificar quais mudanças na alimentação d{SEXO} {NOMECRIANCA} serão necessárias para adotar um estilo de vida saudável. Com mudanças certas e atividade física regular, a maioria das crianças é capaz de chegar a um IMC (índice de massa corporal) saudável aproveitando o potencial de crescimento.";
					break;
				default: // Obesidade
					mensagem = "{NOMERESPONSAVEL}, analisando os dados que você nos informou para a avaliação d{SEXO} {NOMECRIANCA} observamos que {ELEELA} está em obesidade. Com a ajuda do DS Kids e do seu pediatra, você conseguirá identificar quais mudanças na alimentação d{SEXO} {NOMECRIANCA} serão necessárias para adotar um estilo de vida saudável. Com mudanças certas e atividade física regular, a maioria das crianças é capaz de chegar a um IMC (índice de massa corporal) saudável aproveitando o potencial de crescimento.";
					break;
			}

			Descricao = CrescimentoHelpers.FormatarMensagem(mensagem,
				LoginHelper.CurrentCrianca.Nome,
				LoginHelper.CurrentUser.Nome,
				LoginHelper.CurrentCrianca.Sexo);
		}

	}

	public class AvaliacaoViewModelParams
	{

		public AvaliacaoViewModelParams(bool modal, TipoCrescimento tipoCrescimento)
		{
			Modal = modal;
			TipoCrescimento = tipoCrescimento;
		}

		public bool Modal { get; private set; }

		public TipoCrescimento TipoCrescimento { get; private set; }

	}

}
