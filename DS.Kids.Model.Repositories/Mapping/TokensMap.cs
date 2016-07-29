using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DS.Kids.Model.Repositories.Mapping
{
    public class TokensMap : EntityTypeConfiguration<Token>
    {
        public TokensMap()
        {
            // Primary Key
            this.HasKey(t => t.ResponsavelId);

            // Properties
            this.Property(t => t.Valor)
                .IsRequired()
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("Tokens");
            this.Property(t => t.Valor).HasColumnName("Valor");
            this.Property(t => t.ResponsavelId).HasColumnName("IdResponsavel");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.DataCriacao).HasColumnName("DataCriacao").HasColumnType("datetime");

            //// Relationships
            this.HasRequired(t => t.Responsavel)
                .WithOptional(t => t.Token);
        }
    }
}
