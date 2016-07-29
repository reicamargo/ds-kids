using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DS.Kids.Model.Repositories.Mapping
{
    public class ObjetivosMap : EntityTypeConfiguration<Objetivo>
    {
        public ObjetivosMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Objetivos");
            this.Property(t => t.Id).HasColumnName("IdObjetivo");
            this.Property(t => t.IdBrincadeira).HasColumnName("IdBrincadeira");
            this.Property(t => t.Descricao).HasColumnName("Descricao");

            // Relationships
            this.HasRequired(t => t.Brincadeira)
                .WithMany(t => t.Objetivos)
                .HasForeignKey(d => d.IdBrincadeira);          
        }
    }
}
