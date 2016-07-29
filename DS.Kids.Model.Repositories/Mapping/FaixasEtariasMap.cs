using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DS.Kids.Model.Repositories.Mapping
{
    public class FaixasEtariasMap : EntityTypeConfiguration<FaixaEtaria>
    {
        public FaixasEtariasMap()
        {
            // Primary Key
            this.HasKey(t => t.IdFaixaEtaria);

            // Properties
            this.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("FaixasEtarias");
            this.Property(t => t.IdFaixaEtaria).HasColumnName("IdFaixaEtaria");
            this.Property(t => t.MesesDeIdadeInicial).HasColumnName("MesesIdadeInicial");
            this.Property(t => t.MesesDeIdadeFinal).HasColumnName("MesesIdadeFinal");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
        }
    }
}
