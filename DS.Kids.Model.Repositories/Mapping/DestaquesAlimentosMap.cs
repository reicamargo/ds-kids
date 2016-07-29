using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DS.Kids.Model.Repositories.Mapping
{
    public class DestaquesAlimentosMap : EntityTypeConfiguration<DestaqueAlimento>
    {
        public DestaquesAlimentosMap()
        {
            // Primary Key
            this.HasKey(t => t.IdDestaqueAlimento);

            // Properties
            // Table & Column Mappings
            this.ToTable("DestaquesAlimentos", "alimento");
            this.Property(t => t.IdDestaqueAlimento).HasColumnName("IdDestaqueAlimento");
            this.Property(t => t.IdParceiro).HasColumnName("IdParceiro");
            this.Property(t => t.IdAlimento).HasColumnName("IdAlimento");

            // Relationships
            this.HasRequired(t => t.Alimento)
                .WithMany(t => t.DestaquesAlimentos)
                .HasForeignKey(d => d.IdAlimento);
            this.HasRequired(t => t.Parceiro)
                .WithMany(t => t.DestaquesAlimentos)
                .HasForeignKey(d => d.IdParceiro);

        }
    }
}
