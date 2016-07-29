using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DS.Kids.Model.Repositories.Mapping
{
    public class DicasMap : EntityTypeConfiguration<Dica>
    {
        public DicasMap()
        {
            // Primary Key
            this.HasKey(t => t.IdDica);

            // Properties
            this.Property(t => t.Titulo)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Url)
                .IsRequired()
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("Dicas", "conteudo");
            this.Property(t => t.IdDica).HasColumnName("IdDica");
            this.Property(t => t.IdCategoria).HasColumnName("IdCategoria");
            this.Property(t => t.Titulo).HasColumnName("Titulo");
            this.Property(t => t.Url).HasColumnName("Url");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.Destaque).HasColumnName("Destaque");
            this.Property(t => t.IdParceiro).HasColumnName("IdParceiro");

            // Relationships
            this.HasOptional(t => t.Parceiro)
                .WithMany(t => t.Dicas)
                .HasForeignKey(d => d.IdParceiro);

            this.HasRequired(t => t.Categoria)
                .WithMany(t => t.Dicas)
                .HasForeignKey(d => d.IdCategoria);

        }
    }
}
