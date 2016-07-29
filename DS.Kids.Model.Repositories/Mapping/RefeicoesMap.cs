using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DS.Kids.Model.Repositories.Mapping
{
    public class RefeicoesMap : EntityTypeConfiguration<Refeicao>
    {
        public RefeicoesMap()
        {
            // Primary Key
            this.HasKey(t => t.IdRefeicao);

            // Properties
            // Table & Column Mappings
            this.ToTable("Refeicoes", "cardapio");
            this.Property(t => t.IdRefeicao).HasColumnName("IdRefeicao");
            this.Property(t => t.TiposRefeicao).HasColumnName("IdTipoRefeicao");
            this.Property(t => t.IdFaixaEtaria).HasColumnName("IdFaixaEtaria");
            this.Property(t => t.IdParceiro).HasColumnName("IdParceiro");
            this.Property(t => t.DataCriacao).HasColumnName("DataCriacao");
            this.Property(t => t.Ativo).HasColumnName("Ativo");

            // Relationships
            this.HasRequired(t => t.FaixaEtaria)
                .WithMany(t => t.Refeicoes)
                .HasForeignKey(d => d.IdFaixaEtaria);
            
            this.HasOptional(t => t.Parceiro)
                .WithMany(t => t.Refeicoes)
                .HasForeignKey(r => r.IdParceiro);
        }
    }
}
