using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Input;

using BRFX.Core.MessageBox;
using BRFX.Core.Validation;
using BRFX.Core.ViewModels;

using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.PictureChooser;
using Cirrious.MvvmCross.ViewModels;

using DS.Kids.Apps.Core.Analytics;
using DS.Kids.Apps.Core.Helpers;
using DS.Kids.Apps.Core.Messages;
using DS.Kids.Apps.Core.Resources;
using DS.Kids.Model;
using DS.Kids.Model.Services;
using DS.Kids.Model.Validations;

namespace DS.Kids.Apps.Core.ViewModels
{

	public class AdicionarFilhoViewModel : FormViewModel<bool>
	{

		public AdicionarFilhoViewModel()
		{
			var analytics = Mvx.Resolve<IAnalytics>();
			analytics.SendView("AdicionarFilhoView");

			AlturasPossiveis = DefaultHelpers.GetAlturasPossiveis(this);
			PesosPossiveis = DefaultHelpers.GetPesosPossiveis(this);
		}

		private decimal _altura = 1.4M;

		private DateTime? _dataNascimento;

		private string _nome;

		private decimal _peso = 5.0M;

		private byte[] _pictureBytes;

		private string _sexo;

		private string _zippedString64Image;

		private static bool _modal;

		public byte[] PictureBytes
		{
			get
			{
				return _pictureBytes;
			}
			private set
			{
				SetProperty(ref _pictureBytes, value);
			}
		}

		[Required("Preencha o nome de seu filho.")]
		[MaxLength(50, "Tamanho máximo do nome é de 50 caracteres.")]
		public string Nome
		{
			get
			{
				return _nome;
			}
			set
			{
				Set(ref _nome, value);
			}
		}

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

		[Required("Preencha a data de nascimento de seu filho.")]
		[YearRange(-11, -2, "A criança deve ter de 2 a 10 anos")]
		public DateTime? DataNascimento
		{
			get
			{
				return _dataNascimento;
			}
			set
			{
				SetProperty(ref _dataNascimento, value);
			}
		}

		[Required("Selecione o gênero de seu filho.")]
		public string Sexo
		{
			get
			{
				return _sexo;
			}
			set
			{
				Set(ref _sexo, value);
			}
		}

		public ICommand ConcluirCommand { get; private set; }

		public MvxCommand<string> SelectSexoCommand { get; private set; }

		public ICommand SelectPhotoCommand { get; private set; }

		public ObservableCollection<decimal> AlturasPossiveis { get; private set; }

		public ObservableCollection<decimal> PesosPossiveis { get; set; }

		protected override void CreateCommands()
		{
			base.CreateCommands();

			ConcluirCommand = new MvxCommand(ExecuteConcluirCommand);
			SelectSexoCommand = new MvxCommand<string>(ExecuteSelectSexoCommand);
			SelectPhotoCommand = new MvxCommand(ExecuteSelectPhotoCommand);
		}

		private void ExecuteSelectPhotoCommand()
		{
			var pictureChooserTask = Mvx.Resolve<IMvxPictureChooserTask>();
			pictureChooserTask.ChoosePictureFromLibrary(200, 100,
				stream =>
					{
						PictureBytes = ((MemoryStream)stream).ToArray();
						_zippedString64Image = ImageHelper.ByteArrayToZippedString64(PictureBytes);
						Debug.WriteLine("UnZipped size: {0}", PictureBytes.Length);
						Debug.WriteLine("Zipped  size : {0}", _zippedString64Image.Length);
					},
				() => { Debug.WriteLine("Image selection cancelled."); });
		}

		private void ExecuteSelectSexoCommand(string sexo)
		{
			Sexo = sexo;
			Messenger.Publish(new SexChangedMessage(this));
		}

		private async void ExecuteConcluirCommand()
		{
			Dictionary<string, List<ValidationAttribute>> dict = null;
			Validator.Validate(this, Nome, "Nome", ref dict);
			Validator.Validate(this, Altura, "Altura", ref dict);
			Validator.Validate(this, Peso, "Peso", ref dict);
			Validator.Validate(this, DataNascimento, "DataNascimento", ref dict);
			Validator.Validate(this, Sexo, "Sexo", ref dict);

			var messageBox = Mvx.Resolve<IMessageBox>();

			var firstError = GetFirstError(dict);
			if(firstError != null)
			{
				if(firstError is YearRangeAttribute)
				{
					messageBox.Show("O DS Kids foi desenvolvido pensando nas caracteristicas de crianças em " +
									"idade pré escolar e escolar, por isso, menores de 2 anos ou maiores que " +
									"10 anos, a curva de crescimento e os cardápios sugeridos não se aplicam.");
					return;
				}
				messageBox.Show(firstError.ValidationMessage);
				return;
			}

			StartLoading();
			try
			{
				var service = Mvx.Resolve<ICrianca>();
				var result = await service.InserirAsync(new Crianca
															{
																AlturaInicial = Altura,
																DataNascimento = DataNascimento ?? DateTime.UtcNow,
																Nome = Nome,
																PesoInicial = Peso,
																Sexo = Sexo,
																IdResponsavel = LoginHelper.CurrentUser.IdResponsavel,
																ImagemZip = _zippedString64Image
															});

				if(result.ResultCode != ResultCodes.Success)
				{
					Debug.WriteLine("Error - Inserir Criança: " + result.ResultMessage);
					messageBox.Log(result.ResultMessage);
					return;
				}

				LoginHelper.AddCrianca(result.Data, this);

				var tipoCrescimento = TipoCrescimento.Normal;

				var crescimento = result.Data.Crescimentos.FirstOrDefault();
				if(crescimento != null)
				{
					tipoCrescimento = crescimento.TipoCrescimento;
				}

				NavigateTo<AvaliacaoViewModel>(new AvaliacaoViewModelParams(_modal, tipoCrescimento));
				Close(this);
			}
			catch(Exception ex)
			{
				Debug.WriteLine("Error - Inserir Criança: " + ex);
				messageBox.Log(ex);
			}
			finally
			{
				StopLoading();
			}
		}

		protected override bool CanExecuteGoBack()
		{
			return LoginHelper.CurrentCrianca != null;
		}

		public override string GetResourceStringForIndex(string index)
		{
			return AppResources.ResourceManager.GetString(index);
		}

		/// <summary>
		/// Obtém os parâmetros enviado para este view model.
		/// </summary>
		protected override void GetParams(bool modal)
		{
			_modal = modal;
		}

	}

}
