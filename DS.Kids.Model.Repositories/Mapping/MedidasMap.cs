using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DS.Kids.Model.Repositories.Mapping
{
    public class MedidasMap : EntityTypeConfiguration<Medida>
    {
        public MedidasMap()
        {
            // Primary Key
            this.HasKey(t => t.IdMedida);

            // Properties
            this.Property(t => t.NomeSingular)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Medidas", "alimento");
            this.Property(t => t.IdMedida).HasColumnName("IdMedida");
            this.Property(t => t.NomeSingular).HasColumnName("Nome");
            this.Property(t => t.NomePlural).HasColumnName("NomePlural");

            this.Ignore(c => c.Nome);
        }
    }
}
