using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace DS.Kids.Model.Repositories.Mapping
{
    public class AlimentosMedidasFaixasEtariaMap : EntityTypeConfiguration<AlimentoMedidaFaixaEtaria>
    {
        public AlimentosMedidasFaixasEtariaMap()
        {
            // Primary Key
            this.HasKey(t => t.IdAlimentoMedidaFaixaEtaria);

            // Properties
            // Table & Column Mappings
            this.ToTable("AlimentosMedidasFaixasEtarias", "diario");
            //this.Property(t => t.IdAlimentoMedidaFaixaEtaria).HasColumnName("IdAlimentoMedidaFaixaEtaria");
            this.Property(t => t.IdAlimento).HasColumnName("IdAlimento");
            this.Property(t => t.IdMedida).HasColumnName("IdMedida");
            this.Property(t => t.IdFaixaEtaria).HasColumnName("IdFaixaEtaria");
            this.Property(t => t.Quantidade).HasColumnName("Quantidade");
            this.Property(t => t.Semaforo).HasColumnName("Semaforo");

            // Relationships
            this.HasRequired(t => t.Alimento)
                .WithMany(t => t.AlimentosMedidasFaixasEtarias)
                .HasForeignKey(d => d.IdAlimento);
            this.HasRequired(t => t.Medida)
                .WithMany(t => t.AlimentosMedidasFaixasEtarias)
                .HasForeignKey(d => d.IdMedida);
            this.HasRequired(t => t.FaixasEtaria)
                .WithMany(t => t.AlimentosMedidasFaixasEtarias)
                .HasForeignKey(d => d.IdFaixaEtaria);

        }
    }
}
