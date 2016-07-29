using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DS.Kids.Model.Repositories.Mapping
{
    public class RefeicoesItensMap : EntityTypeConfiguration<RefeicaoItem>
    {
        public RefeicoesItensMap()
        {
            // Primary Key
            this.HasKey(t => t.IdRefeicaoItem);

            // Properties
            // Table & Column Mappings
            this.ToTable("RefeicoesItens", "cardapio");
            this.Property(t => t.IdRefeicaoItem).HasColumnName("IdRefeicaoItem");
            this.Property(t => t.IdRefeicao).HasColumnName("IdRefeicao");
            this.Property(t => t.IdAlimento).HasColumnName("IdAlimento");
            this.Property(t => t.IdMedida).HasColumnName("IdMedida");
            this.Property(t => t.Quantidade).HasColumnName("Quantidade");

            // Relationships
            this.HasRequired(t => t.Alimento)
                .WithMany(t => t.RefeicoesItens)
                .HasForeignKey(d => d.IdAlimento);
            this.HasRequired(t => t.Medida)
                .WithMany(t => t.RefeicoesItens)
                .HasForeignKey(d => d.IdMedida);
            this.HasRequired(t => t.Refeicao)
                .WithMany(t => t.RefeicoesItens)
                .HasForeignKey(d => d.IdRefeicao);

        }
    }
}
