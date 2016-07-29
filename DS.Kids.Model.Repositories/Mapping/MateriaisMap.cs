using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DS.Kids.Model.Repositories.Mapping
{
    public class MateriaisMap : EntityTypeConfiguration<Material>
    {
        public MateriaisMap()
        {
            // Primary Key
            this.HasKey(t => t.IdMaterial);

            // Properties
            this.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Materiais");
            this.Property(t => t.IdMaterial).HasColumnName("IdMaterial");
            this.Property(t => t.Descricao).HasColumnName("Descricao");

            this.HasMany(t => t.Brincadeiras)
                .WithMany(t => t.Materiais)
                .Map(m =>
                {
                    m.MapLeftKey("IdMaterial");
                    m.MapRightKey("IdBrincadeira");
                    m.ToTable("BrincadeirasMateriais");
                });
        }
    }
}