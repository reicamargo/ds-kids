using DS.Kids.Model.Repositories.Mapping;
using System.Data.Entity;

namespace DS.Kids.Model.Repositories
{
    public partial class Context : DbContext
    {
        static Context()
        {
            Database.SetInitializer<Context>(null);
        }

        public Context()
            : base("Name=DSKids")
        {
            this.Configuration.ProxyCreationEnabled = false;
#if DEBUG
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
#endif
        }

        public DbSet<Model.Crescimento> Crescimentos { get; set; }
        public DbSet<Model.Crianca> Criancas { get; set; }
        public DbSet<Model.Responsavel> Responsaveis { get; set; }
        public DbSet<Model.Token> Tokens { get; set; }
        public DbSet<Model.LoginSocial> LoginsSociais { get; set; }
        public DbSet<Model.Brincadeira> Brincadeiras { get; set; }
        public DbSet<Model.Material> Materiais { get; set; }
        public DbSet<Model.Objetivo> Objetivos { get; set; }
        public DbSet<Model.Categoria> Categorias { get; set; }
        public DbSet<Model.Dica> Dicas { get; set; }
        public DbSet<Model.Parceiro> Parceiros { get; set; }
        public DbSet<Model.Alimento> Alimentos { get; set; }
        public DbSet<Model.Medida> Medidas { get; set; }
        public DbSet<Model.Refeicao> Refeicoes { get; set; }
        public DbSet<Model.RefeicaoItem> RefeicoesItens { get; set; }
        public DbSet<Model.Paragrafo> Paragrafos { get; set; }
        public DbSet<Model.FaixaEtaria> FaixasEtarias { get; set; }
        public DbSet<Model.RefeicaoDiario> RefeicoesDiario { get; set; }
        public DbSet<Model.RefeicaoGrupo> RefeicoesGrupo { get; set; } 
        public DbSet<Model.AlimentoMedidaFaixaEtaria> AlimentosMedidasFaixasEtarias { get; set; }
        public DbSet<DestaqueAlimento> DestaquesAlimentos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CrescimentosMap());
            modelBuilder.Configurations.Add(new CriancasMap());
            modelBuilder.Configurations.Add(new ResponsaveisMap());
            modelBuilder.Configurations.Add(new TokensMap());
            modelBuilder.Configurations.Add(new LoginSocialMap());
            modelBuilder.Configurations.Add(new BrincadeirasMap());
            modelBuilder.Configurations.Add(new MateriaisMap());
            modelBuilder.Configurations.Add(new ObjetivosMap());
            modelBuilder.Configurations.Add(new CategoriasMap());
            modelBuilder.Configurations.Add(new DicasMap());
            modelBuilder.Configurations.Add(new ParceirosMap());
            modelBuilder.Configurations.Add(new AlimentosMap());
            modelBuilder.Configurations.Add(new MedidasMap());
            modelBuilder.Configurations.Add(new RefeicoesItensMap());
            modelBuilder.Configurations.Add(new RefeicoesMap());
            modelBuilder.Configurations.Add(new FaixasEtariasMap());
            modelBuilder.Configurations.Add(new ParagrafosMap());
            modelBuilder.Configurations.Add(new RefeicoesGruposMap());
            modelBuilder.Configurations.Add(new RefeicoesDiarioMap());
            modelBuilder.Configurations.Add(new AlimentosMedidasFaixasEtariaMap());
            modelBuilder.Configurations.Add(new DestaquesAlimentosMap());
        }
    }
}
