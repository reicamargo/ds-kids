using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DS.Kids.Model.Repositories.Mapping
{
    public class ResponsaveisMap : EntityTypeConfiguration<Responsavel>
    {
        public ResponsaveisMap()
        {
            // Primary Key
            this.HasKey(t => t.IdResponsavel);

            // Properties
            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(256);

            this.Property(t => t.Senha)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.Telefone)
                .HasMaxLength(11);

            // Table & Column Mappings
            this.ToTable("Responsaveis");
            this.Property(t => t.IdResponsavel).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnName("IdResponsavel");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Senha).HasColumnName("Senha");
            this.Property(t => t.Telefone).HasColumnName("Telefone");
            this.Property(t => t.DataCriacao).HasColumnName("DataCriacao").HasColumnType("datetime");
            this.Property(t => t.DataAtualizacao).HasColumnName("DataAtualizacao").HasColumnType("datetime");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.TokenRecuperacaoSenha).HasColumnName("TokenRecuperacaoSenha");
            this.Property(t => t.Optin).HasColumnName("Optin");

            //// Relationships
            this.HasOptional(t => t.Token)
                .WithRequired(t => t.Responsavel);
        }
    }
}
