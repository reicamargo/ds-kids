using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

using BRFX.Core.MessageBox;
using BRFX.Core.Validation;
using BRFX.Core.ViewModels;

using Cirrious.CrossCore;
using Cirrious.CrossCore.Core;
using Cirrious.MvvmCross.ViewModels;

using DS.Kids.Apps.Core.Analytics;
using DS.Kids.Apps.Core.Helpers;
using DS.Kids.Apps.Core.Messages;
using DS.Kids.Apps.Core.Plugins;
using DS.Kids.Apps.Core.Resources;
using DS.Kids.Model;
using DS.Kids.Model.Services;
using DS.Kids.Model.Validations;

namespace DS.Kids.Apps.Core.ViewModels
{

	public class AtualizarPesoAlturaViewModel : FormViewModel<Crescimento>
	{

		private decimal _altura;

		private decimal _peso;

		private string _informativo;

		private Crescimento _crescimento;

		[Required("Preencha a altura de seu filho.")]
		[Range(0.8, 2.0, "Altura entre 0.8m e 2.0m.")]
		public decimal Altura
		{
			get
			{
				return _altura;
			}
			set
			{
				Set(ref _altura, value);
			}
		}

		[Required("Preencha o peso de seu filho.")]
		[Range(5.0, 120.0, "Peso entre 5kg e 120kg.")]
		public decimal Peso
		{
			get
			{
				return _peso;
			}
			set
			{
				Set(ref _peso, value);
			}
		}

		public AtualizarPesoAlturaViewModel()
		{
			var analytics = Mvx.Resolve<IAnalytics>();
			analytics.SendView("AtualizarPesoAlturaView");

			Informativo = string.Format("Insira os dados de {0}", LoginHelper.CurrentCrianca.Nome);

			AlturasPossiveis = DefaultHelpers.GetAlturasPossiveis(this);
			PesosPossiveis = DefaultHelpers.GetPesosPossiveis(this);
		}

		public ICommand AtualizarCommand { get; set; }

		public ObservableCollection<decimal> AlturasPossiveis { get; private set; }

		public ObservableCollection<decimal> PesosPossiveis { get; set; }

		public string Informativo
		{
			get
			{
				return _informativo;
			}
			set
			{
				Set(ref _informativo, value);
			}
		}

		protected override void CreateCommands()
		{
			base.CreateCommands();

			AtualizarCommand = new MvxCommand(ExecuteAtualizarCommand);
		}

		public async void ExecuteAtualizarCommand()
		{
			Dictionary<string, List<ValidationAttribute>> dict = null;
			Validator.Validate(this, Peso, "Peso", ref dict);
			Validator.Validate(this, Altura, "Altura", ref dict);

			var messageBox = Mvx.Resolve<IMessageBox>();

			var firstError = GetFirstError(dict);
			if (firstError != null)
			{
				messageBox.Show(firstError.ValidationMessage);
				return;
			}

			StartLoading();
			try
			{
			    var service = Mvx.Resolve<ICrescimento>();

				Result<Crescimento> result;
				var pesoAltura = new PesoAltura
									{
										IdCrianca = LoginHelper.CurrentCrianca.IdCrianca,
										Altura = Altura,
										Peso = Peso
									};
				if(_crescimento != null)
				{
					pesoAltura.IdCrescimento = _crescimento.IdCrescimento;
					result = await service.AtualizarAsync(pesoAltura);
				}
				else
				{
					result = await service.InserirAsync(pesoAltura);
				}

				if (result.ResultCode != ResultCodes.Success)
				{
					Debug.WriteLine("Error - Inserir/Alterar Crescimento: " + result.ResultMessage);
					messageBox.Log(result.ResultMessage);
					return;
				}

				result.Data.Crianca = null;

				Crescimento ultimoCrescimento;

				// Se for atualização
				if (_crescimento != null)
				{
					ultimoCrescimento = LoginHelper.CurrentCrianca.Crescimentos.LastOrDefault(c => c.IdCrescimento < _crescimento.IdCrescimento);

					_crescimento.Peso = Peso;
					_crescimento.Altura = Altura;
					_crescimento.TipoCrescimento = result.Data.TipoCrescimento;
				}
				else
				{
					ultimoCrescimento = LoginHelper.CurrentCrianca.Crescimentos.LastOrDefault();

					LoginHelper.CurrentCrianca.Crescimentos.Add(result.Data);

					var dispatcher = MvxMainThreadDispatcher.Instance;
					dispatcher.RequestMainThreadAction(() =>
					{
						var localNotifications = Mvx.Resolve<ILocalNotifications>();

						// Deleta se já existe notificação para esta criança
						localNotifications.CancelNotification(CrescimentoHelpers.GetCrescimentoNotificationId(LoginHelper.CurrentCrianca.IdCrianca));

						localNotifications.ScheduleNotification(CrescimentoHelpers.GetCrescimentoNotificationId(LoginHelper.CurrentCrianca.IdCrianca), CrescimentoHelpers.Body, CrescimentoHelpers.AlertStartTime);
					});
				}

				LoginHelper.SaveCurrentUser();

				Messenger.Publish(new NewCrescimentoMessage(this, result.Data));

				string mensagem = "Seu peso foi atualizado! Veja no gráfico como foi a evolução.";

				if (ultimoCrescimento != null && ultimoCrescimento.TipoCrescimento != result.Data.TipoCrescimento)
				{
					switch(result.Data.TipoCrescimento)
					{
						case TipoCrescimento.Desnutricao:
							// Emagreceu
							mensagem = "{NOMERESPONSAVEL}, a partir do peso que você nos informou nessa pesagem, observamos que {ELEELA} atingiu um peso não saudável! Por isso, não deixe de leva-l{SEXO} às consultas de rotina com o pediatra para que, juntos, vocês possam investigar as razões do baixo peso. Continue usando o DS Kids para tornar a alimentação d{SEXO} {NOMECRIANCA} mais saudável.";
							break;
						case TipoCrescimento.Normal:
							// Emagreceu
							mensagem = "Parabéns {NOMERESPONSAVEL}!!! Analisando a última pessagem que você informou, observamos que {SEXO} {NOMECRIANCA} atingiu um peso saudável!!! Com certeza esses novos hábitos irão ajudar no crescimento e desenvolvimento d{SEXO} {NOMECRIANCA}.";
							break;
						case TipoCrescimento.Sobrepeso:
							if (ultimoCrescimento.TipoCrescimento > result.Data.TipoCrescimento)
							{
								// Emagreceu
								mensagem = "Parabéns {NOMERESPONSAVEL}, analisando a última pessagem que você informou, observamos que {SEXO} {NOMECRIANCA} passou da obesidade para o sobrepeso!! Com mudanças certas e atividade física regular, a maioria das crianças é capaz de chegar a um IMC (índice de massa corporal) saudável aproveitando o potencial de crescimento.";
							}
							else
							{
								// Engordou
								mensagem = "Ops! {NOMERESPONSAVEL}, notamos que {SEXO} {NOMECRIANCA} estava em com um peso saudável e em sua última pesagem entrou em sobrepeso! Vale a pena ficar atento a sua alimentação para que {ELEELA} volte a um peso saudável. Pequenas mudanças na alimentação e atividade física regular, vão ajudar {ELEELA} a voltar para um IMC (índice de massa corporal) saudável aproveitando o potencial de crescimento.";
							}
							break;

						case TipoCrescimento.Obesidade:
							// Engordou
							mensagem = "Momento de atenção {NOMERESPONSAVEL}! Notamos que {SEXO} {NOMECRIANCA} passou do estado de sobrepeso para obesidade. O cuidado com a alimentação dele deve ser redobrado! Além da alimentação saudável, atividade física regular vão ajudar {ELEELA} atingir um IMC (índice de massa corporal) saudável aproveitando o potencial de crescimento.";
							break;
					}

					mensagem = CrescimentoHelpers.FormatarMensagem(mensagem, 
						LoginHelper.CurrentCrianca.Nome,
						LoginHelper.CurrentUser.Nome, 
						LoginHelper.CurrentCrianca.Sexo);
				}

				messageBox.Show(mensagem, ok => { ExecuteGoBack(); });
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error - Inserir/Alterar Crescimento: " + ex);
				messageBox.Log(ex);
			}
			finally
			{
				StopLoading();
			}
		}

		public override string GetResourceStringForIndex(string index)
		{
			return AppResources.ResourceManager.GetString(index);
		}

		/// <summary>
		/// Obtém os parâmetros enviado para este view model.
		/// </summary>
		protected override void GetParams(Crescimento crescimento)
		{
			_crescimento = crescimento;

			if(_crescimento != null)
			{
				_altura = _crescimento.Altura;
				_peso = _crescimento.Peso;
			}
			else
			{
				var ultimaAtualizacao = LoginHelper.CurrentCrianca.Crescimentos.OrderByDescending(c => c.DataCriacao).FirstOrDefault();

				if(ultimaAtualizacao != null)
				{
					_altura = ultimaAtualizacao.Altura;
					_peso = ultimaAtualizacao.Peso;
				}
				else
				{
					_altura = 1.4M;
					_peso = 5.0M;
				}
			}
		}

	}

}
