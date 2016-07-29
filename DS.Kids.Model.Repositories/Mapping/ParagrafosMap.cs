using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DS.Kids.Model.Repositories.Mapping
{
    public class ParagrafosMap : EntityTypeConfiguration<Paragrafo>
    {
        public ParagrafosMap()
        {
            // Primary Key
            this.HasKey(t => t.IdParagrafo);

            // Table & Column Mappings
            this.ToTable("Paragrafos", "conteudo");
            this.Property(t => t.IdParagrafo).HasColumnName("IdParagrafo");
            this.Property(t => t.IdDica).HasColumnName("IdDica");
            this.Property(t => t.Texto).HasColumnName("Texto");
            this.Property(t => t.Imagem).HasColumnName("Imagem");
            this.Property(t => t.Video).HasColumnName("Video");            
            this.Property(t => t.TipoParagrafo).HasColumnName("TipoParagrafo");
            this.Property(t => t.Ativo).HasColumnName("Ativo");

            // Relationships
            this.HasRequired(p => p.Dica)
                .WithMany(p => p.Paragrafos)
                .HasForeignKey(p => p.IdDica);

            this.Ignore(p => p.UrlImagem);
        }
    }
}
