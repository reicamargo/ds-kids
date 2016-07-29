using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Windows.Input;
using System.ComponentModel;

using Cirrious.CrossCore.Core;
using Cirrious.CrossCore.IoC;
using Cirrious.MvvmCross.Binding.ExtensionMethods;
using Cirrious.MvvmCross.Binding.Touch.Target;

using Foundation;

using UIKit;

namespace DS.Kids.Apps.iOS
{

	// This class is never actually executed, but when Xamarin linking is enabled it does how to ensure types and properties
	// are preserved in the deployed app
	[Preserve(AllMembers = true)]
	public class LinkerPleaseInclude
	{
		#region Public Methods and Operators

		public void Include(UIButton uiButton)
		{
			uiButton.TouchUpInside += (s, e) =>
									uiButton.SetTitle(uiButton.Title(UIControlState.Normal), UIControlState.Normal);
		}

		public void IncludeEnabled(UIButton uiButton)
		{
			uiButton.Enabled = !uiButton.Enabled;
		}

		public void Include(UIBarButtonItem barButton)
		{
			barButton.Clicked += (s, e) =>
								barButton.Title = barButton.Title + string.Empty;
		}

		public void Include(UITextField textField)
		{
			textField.Text = textField.Text + string.Empty;
			textField.EditingChanged += (sender, args) => { textField.Text = string.Empty; };
		}

		public void Include(UITextView textView)
		{
			textView.Text = textView.Text + string.Empty;
			textView.Changed += (sender, args) => { textView.Text = string.Empty; };
		}

		public void Include(UILabel label)
		{
			label.Text = label.Text + string.Empty;
		}

		public void Include(UIImageView imageView)
		{
			imageView.Image = new UIImage(imageView.Image.CGImage);
		}

		public void Include(UIDatePicker date)
		{
			date.Date = date.Date.AddSeconds(1);
			date.ValueChanged += (sender, args) => { date.Date = (NSDate)DateTime.MaxValue; };
		}

		public void Include(UISlider slider)
		{
			slider.Value = slider.Value + 1;
			slider.ValueChanged += (sender, args) => { slider.Value = 1; };
		}

		public void Include(UIProgressView progress)
		{
			progress.Progress = progress.Progress + 1;
		}

		public void Include(UISwitch sw)
		{
			sw.On = !sw.On;
			sw.ValueChanged += (sender, args) => { sw.On = false; };
		}

		public void Include(INotifyCollectionChanged changed)
		{
			changed.CollectionChanged += (s, e) =>
				{
					var test = string.Format("{0}{1}{2}{3}{4}", e.Action, e.NewItems, e.NewStartingIndex, e.OldItems, e.OldStartingIndex);
					Debug.WriteLine(test);
				};
		}

		public void Include(ICommand command)
		{
			command.CanExecuteChanged += (s, e) =>
				{
					if(command.CanExecute(null))
					{
						command.Execute(null);
					}
				};
		}

		public void Include(MvxPropertyInjector injector)
		{
			if(injector == null)
			{
				throw new ArgumentNullException("injector");
			}

			injector = new MvxPropertyInjector();
			injector.Inject(this);
		}

		public void Include(Cirrious.MvvmCross.Plugins.Color.Touch.Plugin plugin)
		{
			plugin.Load();
			Cirrious.MvvmCross.Plugins.Color.Touch.Plugin x = new Cirrious.MvvmCross.Plugins.Color.Touch.Plugin();
			x.Load();
		}

		public void Include(Cirrious.MvvmCross.Plugins.DownloadCache.Touch.Plugin plugin)
		{
			plugin.Load();
			Cirrious.MvvmCross.Plugins.DownloadCache.Touch.Plugin x = new Cirrious.MvvmCross.Plugins.DownloadCache.Touch.Plugin();
			x.Load();
		}

		public void Include(Cirrious.MvvmCross.Plugins.File.Touch.Plugin plugin)
		{
			plugin.Load();
			Cirrious.MvvmCross.Plugins.File.Touch.Plugin x = new Cirrious.MvvmCross.Plugins.File.Touch.Plugin();
			x.Load();
		}

		public void Include(Cirrious.MvvmCross.Plugins.Visibility.Touch.Plugin plugin)
		{
			plugin.Load();
			Cirrious.MvvmCross.Plugins.Visibility.Touch.Plugin x = new Cirrious.MvvmCross.Plugins.Visibility.Touch.Plugin();
			x.Load();
		}

		public void Include(Cirrious.MvvmCross.Plugins.Visibility.PluginLoader pluginLoader)
		{
			pluginLoader.EnsureLoaded();
			Cirrious.MvvmCross.Plugins.Visibility.PluginLoader x = new Cirrious.MvvmCross.Plugins.Visibility.PluginLoader();
			x.EnsureLoaded();
		}

		public void Include(Cirrious.MvvmCross.Plugins.Json.PluginLoader pluginLoader)
		{
			pluginLoader.EnsureLoaded();
			pluginLoader = new Cirrious.MvvmCross.Plugins.Json.PluginLoader();
			pluginLoader.EnsureLoaded();
		}

		public void Include(Cirrious.MvvmCross.Plugins.Visibility.MvxVisibilityValueConverter converter)
		{
			converter.Convert(null, null, null, null);
			Cirrious.MvvmCross.Plugins.Visibility.MvxVisibilityValueConverter x = new Cirrious.MvvmCross.Plugins.Visibility.MvxVisibilityValueConverter();
			x.Convert(null, null, null, null);
		}

		public void Include(Cirrious.MvvmCross.Plugins.Visibility.MvxInvertedVisibilityValueConverter converter)
		{
			converter.Convert(null, null, null, null);
			Cirrious.MvvmCross.Plugins.Visibility.MvxInvertedVisibilityValueConverter x = new Cirrious.MvvmCross.Plugins.Visibility.MvxInvertedVisibilityValueConverter();
			x.Convert(null, null, null, null);
		}

		public void Include(Cirrious.MvvmCross.Plugins.File.Touch.MvxTouchFileStore mvxTouchFileStore)
		{
			mvxTouchFileStore.DeleteFile("");
			Cirrious.MvvmCross.Plugins.File.Touch.MvxTouchFileStore x = new Cirrious.MvvmCross.Plugins.File.Touch.MvxTouchFileStore();
			x.DeleteFile("");
		}

		public void Include(Cirrious.MvvmCross.Plugins.DownloadCache.MvxDynamicImageHelper<UIImage> imageHelper)
		{
			imageHelper.ImageChanged += (sender, args) => { imageHelper.Dispose(); };
			var x = new Cirrious.MvvmCross.Plugins.DownloadCache.MvxDynamicImageHelper<UIImage>
						{
							ErrorImagePath = ""
						};
			x.Dispose();
		}

		public void Include(INotifyPropertyChanged notifyPropertyChanged)
		{
			notifyPropertyChanged.PropertyChanged += (sender, args) =>
				{
					notifyPropertyChanged.PropertyChanged -= null;
				};
		}

		public void Include(Cirrious.MvvmCross.Plugins.Json.MvxJsonConverter jsonConverter)
		{
			jsonConverter.SerializeObject(null);
			jsonConverter = new Cirrious.MvvmCross.Plugins.Json.MvxJsonConverter();
			jsonConverter.DeserializeObject<string>("");
		}

		public void Include(MvxUIDatePickerDateTargetBinding x)
		{
			x.DisposeIfDisposable();
			x = new MvxUIDatePickerDateTargetBinding(null, null);
			x.DisposeIfDisposable();
		}

		public void Include(Cirrious.MvvmCross.Plugins.PictureChooser.Touch.Plugin x)
		{
			x.DisposeIfDisposable();
			x = new Cirrious.MvvmCross.Plugins.PictureChooser.Touch.Plugin();
			x.DisposeIfDisposable();
		}

		public void Include(Cirrious.MvvmCross.Plugins.PictureChooser.Touch.MvxImagePickerTask x)
		{
			x.DisposeIfDisposable();
			x = new Cirrious.MvvmCross.Plugins.PictureChooser.Touch.MvxImagePickerTask();
			x.DisposeIfDisposable();
		}

		public void Include(Cirrious.MvvmCross.Plugins.PictureChooser.Touch.MvxInMemoryImageValueConverter x)
		{
			x.DisposeIfDisposable();
			x = new Cirrious.MvvmCross.Plugins.PictureChooser.Touch.MvxInMemoryImageValueConverter();
			x.DisposeIfDisposable();
		}

		public void Include(Cirrious.MvvmCross.Binding.Combiners.MvxFormatValueCombiner x)
		{
			x.DisposeIfDisposable();
			x = new Cirrious.MvvmCross.Binding.Combiners.MvxFormatValueCombiner();
			object y;
			x.TryGetValue(null, out y);
			x.SetValue(null, y);
		}

		public void Include(MvxUIButtonTitleTargetBinding x)
		{
			x.SetValue(null);
			x = new MvxUIButtonTitleTargetBinding(null);
			x.SetValue(null);
		}

		public void Include(Cirrious.MvvmCross.Binding.Bindings.Target.MvxPropertyInfoTargetBinding x)
		{
			x.SetValue(null);
			x = new Cirrious.MvvmCross.Binding.Bindings.Target.MvxPropertyInfoTargetBinding(null, null);
			x.SetValue(null);
		}

		public void Include(Cirrious.MvvmCross.Plugins.Color.MvxNativeColorValueConverter x)
		{
			x.Convert(null, null, null, null);
			x = new Cirrious.MvvmCross.Plugins.Color.MvxNativeColorValueConverter();
			x.Convert(null, null, null, null);
		}

		public void Include(Cirrious.MvvmCross.Plugins.WebBrowser.Touch.MvxWebBrowserTask x)
		{
			x.ShowWebPage(null);
			x = new Cirrious.MvvmCross.Plugins.WebBrowser.Touch.MvxWebBrowserTask();
			x.ShowWebPage(null);
		}

		public void Include(Cirrious.MvvmCross.Plugins.WebBrowser.Touch.Plugin x)
		{
			x.Load();
			x = new Cirrious.MvvmCross.Plugins.WebBrowser.Touch.Plugin();
			x.Load();
		}

		public void Include(Model.Cardapio cardapio)
		{
			if (cardapio == null)
			{
				throw new ArgumentNullException("cardapio");
			}
			cardapio = new Model.Cardapio();
			cardapio.ConvertToBoolean();
		}

		public void Include(Model.Refeicao refeicao)
		{
			if (refeicao == null)
			{
				throw new ArgumentNullException("refeicao");
			}
			refeicao = new Model.Refeicao();
			refeicao.ConvertToBoolean();
		}

		public void Include(Model.Parceiro parceiro)
		{
			if (parceiro == null)
			{
				throw new ArgumentNullException("parceiro");
			}
			parceiro = new Model.Parceiro();
			parceiro.ConvertToBoolean();
		}

		public void Include(Model.RefeicaoItem refeicaoItem)
		{
			if (refeicaoItem == null)
			{
				throw new ArgumentNullException("refeicaoItem");
			}
			refeicaoItem = new Model.RefeicaoItem();
			refeicaoItem.ConvertToBoolean();
		}

		public void Include(Model.Medida medida)
		{
			if (medida == null)
			{
				throw new ArgumentNullException("medida");
			}
			medida = new Model.Medida();
			medida.ConvertToBoolean();
		}

		public void Include(Model.Alimento alimento)
		{
			if (alimento == null)
			{
				throw new ArgumentNullException("alimento");
			}
			alimento = new Model.Alimento();
			alimento.ConvertToBoolean();
		}

		public void Include(Model.FaixaEtaria faixaEtaria)
		{
			if (faixaEtaria == null)
			{
				throw new ArgumentNullException("faixaEtaria");
			}
			faixaEtaria = new Model.FaixaEtaria();
			faixaEtaria.ConvertToBoolean();
		}

		public void Include(Model.Dica dica)
		{
			if(dica == null)
			{
				throw new ArgumentNullException("dica");
			}
			dica = new Model.Dica();
			dica.ConvertToBoolean();
		}

		public void Include(Model.Categoria categoria)
		{
			if(categoria == null)
			{
				throw new ArgumentNullException("categoria");
			}
			categoria = new Model.Categoria();
			categoria.ConvertToBoolean();
		}

		public void Include(Model.Paragrafo paragrafo)
		{
			if(paragrafo == null)
			{
				throw new ArgumentNullException("paragrafo");
			}
			paragrafo = new Model.Paragrafo();
			paragrafo.ConvertToBoolean();
		}

		public void Include(List<Model.Cardapio> cardapios)
		{
			if (cardapios == null)
			{
				throw new ArgumentNullException("cardapios");
			}
			cardapios = new List<Model.Cardapio>();
			cardapios.ConvertToBoolean();
		}

		public void Include(List<Model.Refeicao> refeicoes)
		{
			if (refeicoes == null)
			{
				throw new ArgumentNullException("refeicoes");
			}
			refeicoes = new List<Model.Refeicao>();
			refeicoes.ConvertToBoolean();
		}

		public void Include(List<Model.Parceiro> parceiros)
		{
			if (parceiros == null)
			{
				throw new ArgumentNullException("parceiros");
			}
			parceiros = new List<Model.Parceiro>();
			parceiros.ConvertToBoolean();
		}

		public void Include(List<Model.RefeicaoItem> refeicoesItem)
		{
			if (refeicoesItem == null)
			{
				throw new ArgumentNullException("refeicoesItem");
			}
			refeicoesItem = new List<Model.RefeicaoItem>();
			refeicoesItem.ConvertToBoolean();
		}

		public void Include(List<Model.Medida> medidas)
		{
			if (medidas == null)
			{
				throw new ArgumentNullException("medidas");
			}
			medidas = new List<Model.Medida>();
			medidas.ConvertToBoolean();
		}

		public void Include(List<Model.Alimento> alimentos)
		{
			if (alimentos == null)
			{
				throw new ArgumentNullException("alimentos");
			}
			alimentos = new List<Model.Alimento>();
			alimentos.ConvertToBoolean();
		}

		public void Include(List<Model.FaixaEtaria> faixasEtarias)
		{
			if (faixasEtarias == null)
			{
				throw new ArgumentNullException("faixasEtarias");
			}
			faixasEtarias = new List<Model.FaixaEtaria>();
			faixasEtarias.ConvertToBoolean();
		}

		public void Include(List<Model.Dica> dicas)
		{
			if(dicas == null)
			{
				throw new ArgumentNullException("dicas");
			}
			dicas = new List<Model.Dica>();
			dicas.ConvertToBoolean();
		}

		public void Include(List<Model.Crescimento> crescimentos)
		{
			if(crescimentos == null)
			{
				throw new ArgumentNullException("crescimentos");
			}
			crescimentos = new List<Model.Crescimento>();
			crescimentos.ConvertToBoolean();
		}

		public void Include(List<Model.Brincadeira> brincadeiras)
		{
			if(brincadeiras == null)
			{
				throw new ArgumentNullException("brincadeiras");
			}
			brincadeiras = new List<Model.Brincadeira>();
			brincadeiras.ConvertToBoolean();
		}

		public void Include(List<Model.Crianca> criancas)
		{
			if(criancas == null)
			{
				throw new ArgumentNullException("criancas");
			}
			criancas = new List<Model.Crianca>();
			criancas.ConvertToBoolean();
		}

		public void Include(List<Model.Categoria> categorias)
		{
			if(categorias == null)
			{
				throw new ArgumentNullException("categorias");
			}
			categorias = new List<Model.Categoria>();
			categorias.ConvertToBoolean();
		}

		public void Include(List<Model.Paragrafo> paragrafos)
		{
			if(paragrafos == null)
			{
				throw new ArgumentNullException("paragrafos");
			}
			paragrafos = new List<Model.Paragrafo>();
			paragrafos.ConvertToBoolean();
		}

		public void Include(MvxUISearchBarTextTargetBinding x)
		{
			x.DisposeIfDisposable();
			x = new MvxUISearchBarTextTargetBinding(null, null);
			x.DisposeIfDisposable();
		}

		#endregion
	}

}
