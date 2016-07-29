using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DS.Kids.Model.Repositories.Mapping
{
    public class BrincadeirasMap : EntityTypeConfiguration<Brincadeira>
    {
        public BrincadeirasMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Titulo)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Brincadeiras");

            this.Property(t => t.Id)
                .HasColumnName("IdBrincadeira")
                .HasColumnType("int")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Titulo)
                .HasColumnName("Titulo")
                .HasColumnType("varchar")
                .HasMaxLength(100);

            this.Property(t => t.FaixaEtaria)
                .HasColumnName("FaixaEtaria")
                .HasColumnType("varchar")
                .HasMaxLength(50);

            this.Property(t => t.Ambiente)
                .HasColumnName("Ambiente")
                .HasColumnType("int");

            this.Property(t => t.Instrucoes)
                .HasColumnName("Instrucoes")
                .HasColumnType("varchar");

            this.Property(t => t.NomeImagem)
                .HasColumnName("NomeImagem")
                .HasColumnType("varchar")
                .HasMaxLength(250);

            this.Property(t => t.Imagem)
                .HasColumnName("Imagem")
                .HasColumnType("varbinary");

            this.Property(t => t.Ativo)
                .HasColumnName("Ativo")
                .HasColumnType("bit");

            this.Ignore(t => t.UrlImagem);

            this.HasMany(t => t.Materiais)
                .WithMany(t => t.Brincadeiras)
                .Map(m =>
                {
                    m.ToTable("BrincadeirasMateriais");
                    m.MapLeftKey("IdBrincadeira");
                    m.MapRightKey("IdMaterial");
                });
        }
    }
}