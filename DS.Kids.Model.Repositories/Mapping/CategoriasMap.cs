using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DS.Kids.Model.Repositories.Mapping
{
    public class CategoriasMap : EntityTypeConfiguration<Categoria>
    {
        public CategoriasMap()
        {
            // Primary Key
            this.HasKey(t => t.IdCategoria);

            // Table & Column Mappings
            this.ToTable("Categorias", "conteudo");
            this.Property(t => t.IdCategoria)
                .HasColumnName("IdCategoria")
                .HasColumnType("int")
                .IsRequired();

            this.Property(t => t.Nome)
                .HasColumnName("Nome")
                .HasMaxLength(50)
                .IsRequired();

            this.Property(t => t.Destaque)
                .HasColumnName("Destaque")
                .HasColumnType("bit")
                .IsRequired();
            
            this.Property(t => t.Ativo)
                .HasColumnName("Ativo")
                .HasColumnType("bit")
                .IsRequired();

            this.Property(t => t.NomeImagem)
                .HasColumnName("NomeImagem")
                .HasColumnType("varchar")
                .HasMaxLength(250);

            this.Property(t => t.Imagem)
                .HasColumnName("Imagem")
                .HasColumnType("varbinary");

            this.Ignore(t => t.UrlImagem);
        }
    }
}