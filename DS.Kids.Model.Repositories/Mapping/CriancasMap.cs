using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DS.Kids.Model.Repositories.Mapping
{
    public class CriancasMap : EntityTypeConfiguration<Crianca>
    {
        public CriancasMap()
        {
            // Primary Key
            this.HasKey(t => t.IdCrianca);

            // Table & Column Mappings
            this.ToTable("Criancas");
            this.Property(t => t.IdCrianca)
                .HasColumnName("IdCrianca")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.IdResponsavel)
                .HasColumnName("IdResponsavel");

            this.Property(t => t.Nome)
                .HasColumnName("Nome");

            this.Property(t => t.DataNascimento)
                .HasColumnName("DataNascimento")
                .HasColumnType("datetime");

            this.Property(t => t.Sexo)
                .HasColumnName("Sexo");

            this.Property(t => t.PesoInicial)
                .HasColumnName("PesoInicial");

            this.Property(t => t.AlturaInicial)
                .HasColumnName("AlturaInicial");

            this.Property(t => t.NomeImagem)
                .HasColumnName("NomeImagem");

            this.Property(t => t.Imagem)
                .HasColumnName("Imagem");

            this.Property(t => t.DataCriacao)
                .HasColumnName("DataCriacao")
                .HasColumnType("datetime");

            this.Property(t => t.DataAtualizacao)
                .HasColumnName("DataAtualizacao")
                .HasColumnType("datetime");

            this.Property(t => t.Ativo)
                .HasColumnName("Ativo");

            // Relationships
            this.HasRequired(t => t.Responsavel)
                .WithMany(t => t.Criancas)
                .HasForeignKey(d => d.IdResponsavel);

            this.Ignore(c => c.ImagemZip);
            this.Ignore(c => c.UrlImagem);
            this.Ignore(c => c.MesesDeIdade);
            this.Ignore(c => c.ImcInicial);
        }
    }
}