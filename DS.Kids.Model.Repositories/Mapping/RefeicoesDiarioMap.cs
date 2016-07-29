using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace DS.Kids.Model.Repositories.Mapping
{
    public class RefeicoesDiarioMap : EntityTypeConfiguration<RefeicaoDiario>
    {
        public RefeicoesDiarioMap()
        {
            // Primary Key
            this.HasKey(t => t.IdRefeicao);

            // Properties
            // Table & Column Mappings
            this.ToTable("Refeicoes", "diario");
            this.Property(t => t.IdRefeicao).HasColumnName("IdRefeicao");
            this.Property(t => t.IdTipoRefeicao).HasColumnName("IdTipoRefeicao");
            this.Property(t => t.IdCrianca).HasColumnName("IdCrianca");
            this.Property(t => t.DataCriacao).HasColumnName("DataCriacao");

            // Relationships
            this.HasRequired(t => t.Crianca)
                .WithMany(t => t.RefeicoesDiario)
                .HasForeignKey(d => d.IdCrianca);

            this.Ignore(t => t.TipoRefeicao);
        }
    }
}
