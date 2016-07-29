using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace DS.Kids.Model.Repositories.Mapping
{
    public class RefeicoesGruposMap : EntityTypeConfiguration<RefeicaoGrupo>
    {
        public RefeicoesGruposMap()
        {
            // Primary Key
            this.HasKey(t => t.IdRefeicaoGrupo);

            // Properties
            // Table & Column Mappings
            this.ToTable("RefeicoesGrupos", "diario");
            this.Property(t => t.IdRefeicao).HasColumnName("IdRefeicao");
            this.Property(t => t.IdGrupo).HasColumnName("IdGrupo");

            // Relationships
            this.HasRequired(t => t.Grupo)
                .WithMany(t => t.RefeicoesGrupos)
                .HasForeignKey(d => d.IdGrupo);

            this.HasMany(r => r.Alimentos)
                .WithMany(a => a.RefeicoesGrupos)
                .Map(t =>
                {
                    t.MapLeftKey("IdRefeicaoGrupo");
                    t.MapRightKey("IdAlimento");
                    t.ToTable("RefeicoesGruposAlimentos", "diario");
                });

            this.HasRequired(t => t.RefeicaoDiario)
                .WithMany(t => t.RefeicoesGrupos)
                .HasForeignKey(d => d.IdRefeicao);
            
            this.Ignore(t => t.TipoGrupoRefeicao)
                .Ignore(t => t.Sugerido);
        }
    }
}
