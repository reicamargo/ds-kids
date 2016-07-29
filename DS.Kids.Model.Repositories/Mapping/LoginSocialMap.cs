using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DS.Kids.Model.Repositories.Mapping
{
    public class LoginSocialMap : EntityTypeConfiguration<LoginSocial>
    {
        public LoginSocialMap()
        {
            // Primary Key
            this.HasKey(t => t.IdResponsavel);

            // Properties
            this.Property(t => t.Chave)
                .IsRequired()
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("LoginsSociais");
            this.Property(t => t.Chave).HasColumnName("Chave");
            this.Property(t => t.IdResponsavel).HasColumnName("IdResponsavel");
            this.Property(t => t.RedeSocial).HasColumnName("RedeSocial");
            this.Property(t => t.DataCriacao).HasColumnName("DataCriacao").HasColumnType("datetime");

            //// Relationships
            this.HasRequired(t => t.Responsavel)
                .WithOptional(t => t.LoginSocial);

            this.Ignore(t => t.Nome);
            this.Ignore(t => t.Email);
        }
    }
}
