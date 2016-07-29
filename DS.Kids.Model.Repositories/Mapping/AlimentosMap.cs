using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DS.Kids.Model.Repositories.Mapping
{
    public class AlimentosMap : EntityTypeConfiguration<Alimento>
    {
        public AlimentosMap()
        {
            // Primary Key
            this.HasKey(t => t.IdAlimento);

            // Properties
            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Alimentos", "alimento");
            this.Property(t => t.IdAlimento).HasColumnName("IdAlimento");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.IdDica).HasColumnName("IdDica");
            this.Property(t => t.IdGrupo).HasColumnName("IdGrupo");
            this.Property(t => t.Ativo).HasColumnName("Ativo");

            // Relationships
            this.HasOptional(t => t.Dica)
                .WithMany(t => t.Alimentos)
                .HasForeignKey(d => d.IdDica);
            this.HasOptional(t => t.Grupo)
                .WithMany(t => t.Alimentos)
                .HasForeignKey(d => d.IdGrupo);

            this.Ignore(t => t.AlimentoMedidaFaixaEtaria);

            this.Ignore(t => t.Destaque);
        }
    }
}
