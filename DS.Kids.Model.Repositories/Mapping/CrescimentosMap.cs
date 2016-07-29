using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DS.Kids.Model.Repositories.Mapping
{
    public class CrescimentosMap : EntityTypeConfiguration<Crescimento>
    {
        public CrescimentosMap()
        {
            this.ToTable("Crescimentos");
            
            this.HasKey(t => t.IdCrescimento);
                       
            this.Property(t => t.IdCrescimento)
                .HasColumnName("IdCrescimento")
                .HasColumnType("bigint")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.IdCrianca)
                .HasColumnName("IdCrianca")
                .HasColumnType("int")
                .IsRequired();

            this.Property(t => t.Peso)
                .HasColumnName("Peso")
                .HasColumnType("decimal")
                .HasPrecision(5, 2);
            
            this.Property(t => t.Altura)
                .HasColumnName("Altura")
                .HasColumnType("decimal")
                .HasPrecision(5, 2);

            this.Property(t => t.MesesDeIdade)
                .HasColumnName("MesesDeIdade")
                .HasColumnType("int")
                .IsRequired();

            this.Property(t => t.TipoCrescimento)
                .HasColumnName("IdTipoCrescimento")
                .HasColumnType("int")
                .IsRequired();
            
            this.Property(t => t.DataCriacao)
                .HasColumnName("DataCriacao")
                .HasColumnType("datetime");
            
            this.Property(t => t.DataAtualizacao)
                .HasColumnName("DataAtualizacao")
                .HasColumnType("datetime");
        }
    }
}