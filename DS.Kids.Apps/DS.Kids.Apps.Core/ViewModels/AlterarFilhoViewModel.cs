using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;

using BRFX.Core.MessageBox;
using BRFX.Core.Validation;
using BRFX.Core.ViewModels;

using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.PictureChooser;
using Cirrious.MvvmCross.ViewModels;

using DS.Kids.Apps.Core.Analytics;
using DS.Kids.Apps.Core.Helpers;
using DS.Kids.Apps.Core.Resources;
using DS.Kids.Model;
using DS.Kids.Model.Services;
using DS.Kids.Model.Validations;

namespace DS.Kids.Apps.Core.ViewModels
{

	public class AlterarFilhoViewModel : FormViewModel<Crianca>
	{
		#region Fields

		private bool _changed;

		private Crianca _crianca;

		private DateTime? _dataNascimento;

		private string _nome;

		private string _selecionarButtonText;

		private byte[] _pictureBytes;

		private string _zippedString64Image;

		public ICommand SelectPhotoCommand { get; private set; }

		#endregion

		#region Constructors and Destructors

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

		public AlterarFilhoViewModel()
		{
			var analytics = Mvx.Resolve<IAnalytics>();
			analytics.SendView("AlterarFilhoView");
		}

		#endregion

		#region Public Properties

		public bool Changed
		{
			get
			{
				return _changed;
			}
			private set
			{
				Set(ref _changed, value);
				if(value)
				{
					SelecionarButtonText = "Salvar";
				}
				else
				{
					SelecionarButtonText = "Selecionar";
				}
			}
		}

		public Crianca Crianca
		{
			get
			{
				return _crianca;
			}
			private set
			{
				SetProperty(ref _crianca, value);
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
				Changed = true;
			}
		}

		public MvxCommand ExcluirCommand { get; private set; }

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
				Changed = true;
			}
		}

		public string SelecionarButtonText
		{
			get
			{
				return _selecionarButtonText;
			}
			private set
			{
				Set(ref _selecionarButtonText, value);
			}
		}

		public MvxCommand SelecionarOuSalvarCommand { get; private set; }

		#endregion

		#region Public Methods and Operators

		public override string GetResourceStringForIndex(string index)
		{
			return AppResources.ResourceManager.GetString(index);
		}

		#endregion

		#region Methods

		protected override void CreateCommands()
		{
			base.CreateCommands();

			ExcluirCommand = new MvxCommand(ExecuteExcluirCommand);
			SelecionarOuSalvarCommand = new MvxCommand(ExecuteSelecionarOuSalvarCommand);
			SelectPhotoCommand = new MvxCommand(ExecuteSelectPhotoCommand);
		}

		private void ExecuteSelectPhotoCommand()
		{
			var pictureChooserTask = Mvx.Resolve<IMvxPictureChooserTask>();
			pictureChooserTask.ChoosePictureFromLibrary(200, 100,
				stream =>
					{
						Changed = true;
						PictureBytes = ((MemoryStream)stream).ToArray();
						_zippedString64Image = ImageHelper.ByteArrayToZippedString64(PictureBytes);
						Debug.WriteLine("UnZipped size: {0}", PictureBytes.Length);
						Debug.WriteLine("Zipped  size : {0}", _zippedString64Image.Length);
					},
				() => { Debug.WriteLine("Image selection cancelled."); });
		}

		protected override void GetParams(Crianca crianca)
		{
			Crianca = crianca;

			_nome = crianca.Nome;
			_dataNascimento = crianca.DataNascimento;

			Changed = false;
		}

		private void ExecuteExcluirCommand()
		{
			var messageBox = Mvx.Resolve<IMessageBox>();

			if(LoginHelper.CurrentUser.Criancas.Count <= 1)
			{
				messageBox.Show("Você deve ter pelo menos um filho cadastrado.");
			}
			else
			{
				messageBox.Show("Tem certeza que deseja excluir este filho?", ok =>
					{
						if(ok)
						{
							ExcluirFilho();
						}
					}, "Tem certeza?", MessageBoxButtons.OkCancel);
			}
		}

		private async void ExcluirFilho()
		{
			var messageBox = Mvx.Resolve<IMessageBox>();

			StartLoading();
			try
			{
				var service = Mvx.Resolve<ICrianca>();
				var result = await service.ExcluirAsync(Crianca.IdCrianca);

				if (result.ResultCode != ResultCodes.Success)
				{
					Debug.WriteLine("Error - Excluir Criança: " + result.ResultMessage);
					messageBox.Log(result.ResultMessage);
					return;
				}

				LoginHelper.RemoveCrianca(Crianca, this);

				Close(this);
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error - Excluir Criança: " + ex);
				messageBox.Log(ex);
			}
			finally
			{
				StopLoading();
			}
		}

		private async void ExecuteSelecionarOuSalvarCommand()
		{
			if(Changed)
			{
				await SaveCrianca();
			}
			else
			{
				LoginHelper.UpdateCurrentCrianca(Crianca.IdCrianca, this);
				Close(this);
			}
		}

		private async Task SaveCrianca()
		{
			Dictionary<string, List<ValidationAttribute>> dict = null;
			Validator.Validate(this, Nome, "Nome", ref dict);
			Validator.Validate(this, DataNascimento, "DataNascimento", ref dict);

			var messageBox = Mvx.Resolve<IMessageBox>();

			var firstError = GetFirstError(dict);
			if (firstError != null)
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

			Crianca.Nome = Nome;
			if(DataNascimento != null)
			{
				Crianca.DataNascimento = DataNascimento.Value;
			}

			StartLoading();
			try
			{
				var service = Mvx.Resolve<ICrianca>();

				Crianca.ImagemZip = _zippedString64Image;
				Crianca.Nome = Nome;
				if(DataNascimento != null)
				{
					Crianca.DataNascimento = DataNascimento.Value;
				}

				var result = await service.AtualizarAsync(Crianca);

				if(result.ResultCode != ResultCodes.Success)
				{
					Debug.WriteLine("Error - Atualizar Criança: " + result.ResultMessage);
					messageBox.Log(result.ResultMessage);
					return;
				}

				Crianca.ImagemZip = null;
				Crianca.NomeImagem = result.Data.NomeImagem;

				LoginHelper.UpdateCrianca(result.Data, this);

				Changed = false;
			}
			catch(Exception ex)
			{
				Debug.WriteLine("Error - Atualizar Criança: " + ex);
				messageBox.Log(ex);
			}
			finally
			{
				Crianca.ImagemZip = null;
				StopLoading();
			}
		}

		#endregion
	}

}
