using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace DS.Kids.Model.Repositories.Mapping
{
    public class GruposMap : EntityTypeConfiguration<Grupo>
    {
        public GruposMap()
        {
            // Primary Key
            this.HasKey(t => t.GrupoId);

            // Properties
            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Grupos", "diario");
            this.Property(t => t.GrupoId).HasColumnName("IdGrupo");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
        }
    }
}
