using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DS.Kids.Model.Repositories.Mapping
{
    public class ParceirosMap : EntityTypeConfiguration<Parceiro>
    {
        public ParceirosMap()
        {
            // Primary Key
            this.HasKey(t => t.IdParceiro);

            // Properties
            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Url)
                .IsRequired()
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("Parceiros");
            this.Property(t => t.IdParceiro).HasColumnName("IdParceiro");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.Url).HasColumnName("Url");
            this.Property(t => t.Tipo).HasColumnName("Tipo");
            this.Property(t => t.Destaque).HasColumnName("Destaque");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.Imagem).HasColumnName("Imagem");
            this.Property(t => t.NomeImagem).HasColumnName("NomeImagem");
            this.Property(t => t.Icone).HasColumnName("Icone");
            this.Property(t => t.NomeIcone).HasColumnName("NomeIcone");

            this.Ignore(t => t.UrlImagem);
            this.Ignore(t => t.UrlIcone);
        }
    }
}
