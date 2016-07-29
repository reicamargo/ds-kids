using Cirrious.CrossCore;

using DS.Kids.Model.Communication;

namespace DS.Kids.Apps.Core.Helpers
{
    public static class ServicesHelpers
    {
        public static void Initialize()
        {
            Mvx.RegisterSingleton<Model.Services.IAlimento>(new Alimentos());
            Mvx.RegisterSingleton<Model.Services.ICardapio>(new Cardapios());
            Mvx.RegisterSingleton<Model.Services.ICrescimento>(new Crescimentos());
            Mvx.RegisterSingleton<Model.Services.ICrianca>(new Criancas());
            Mvx.RegisterSingleton<Model.Services.IDiario>(new Diarios());
            Mvx.RegisterSingleton<Model.Services.ILogin>(new Login());
            Mvx.RegisterSingleton<Model.Services.IOptin>(new Optin());
            Mvx.RegisterSingleton<Model.Services.IResponsavel>(new Responsaveis());
            Mvx.RegisterSingleton<Model.Services.ISenha>(new Senha());
        }
    }
}
