using Microsoft.Practices.Unity;

using Services = DS.Kids.Model.Services;
using Repositories = DS.Kids.Model.Repositories;

namespace DS.Kids.API
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        public static void RegisterAllTypes(IUnityContainer container)
        {
            /*Events*/
            RegisterEventsTypes(container);

            /*Services*/
            RegisterServicesTypes(container);

            /*Repositories*/
            RegisterRepositoriesTypes(container);
        }

        public static void RegisterEventsTypes(IUnityContainer container)
        {
            container.RegisterType<Services.Events.ISenha, DS.Kids.API.Events.Senha>();        
        }

        public static void RegisterServicesTypes(IUnityContainer container)
        {
            container.RegisterType<Services.IAlimento, Services.Alimento>();
            container.RegisterType<Services.ICardapio, Services.Cardapio>();
            container.RegisterType<Services.ICrescimento, Services.Crescimento>();
            container.RegisterType<Services.ICrianca, Services.Crianca>();
            container.RegisterType<Services.ILogin, Services.Login>();
            container.RegisterType<Services.IResponsavel, Services.Responsavel>();
            container.RegisterType<Services.ISenha, Services.Senha>();
            container.RegisterType<Services.IDiario, Services.Diario>();
            container.RegisterType<Services.IOptin, Services.Optin>();
        }

        public static void RegisterRepositoriesTypes(IUnityContainer container)
        {
            container.RegisterType<Repositories.IAlimentos, Repositories.Alimentos>();
            container.RegisterType<Repositories.ICrescimentos, Repositories.Crescimentos>();
            container.RegisterType<Repositories.ICriancas, Repositories.Criancas>();
            container.RegisterType<Repositories.IResponsaveis, Repositories.Responsaveis>();
            container.RegisterType<Repositories.ITokens, Repositories.Tokens>();
            container.RegisterType<Repositories.ILoginsSociais, Repositories.LoginsSociais>();
            container.RegisterType<Repositories.IRefeicoes, Repositories.Refeicoes>();
            container.RegisterType<Repositories.IBrincadeiras, Repositories.Brincadeiras>();
            container.RegisterType<Repositories.ICategorias, Repositories.Categorias>();
            container.RegisterType<Repositories.IDicas, Repositories.Dicas>();
            container.RegisterType<Repositories.IParceiros, Repositories.Parceiros>();
            container.RegisterType<Repositories.IParagrafos, Repositories.Paragrafos>();
            container.RegisterType<Repositories.IRefeicoesDiarios, Repositories.RefeicoesDiarios>();
            container.RegisterType<Repositories.IRefeicoesGrupos, Repositories.RefeicoesGrupos>();

            container.RegisterInstance<Repositories.IParceiro>(Model.ParceiroSingleton.Instance);
        }
    }
}
