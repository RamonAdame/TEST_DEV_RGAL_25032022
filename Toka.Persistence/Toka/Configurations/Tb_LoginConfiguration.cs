using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toka.Domain.Models;

namespace Toka.Persistence.Toka.Configurations
{
    public class Tb_LoginConfiguration : IEntityTypeConfiguration<Tb_Login>
    {
        public void Configure(EntityTypeBuilder<Tb_Login> builder)
        {
            builder.ToTable("Tb_Login");
            builder.HasKey(x => x.IdUser).HasName("PK__Tb_Login__B7C92638571A57F9").IsClustered();

            builder.Property(x => x.IdUser).HasColumnName(@"IdPersonaFisica").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(x => x.Username).HasColumnName(@"Username").HasColumnType("varchar(12)").IsRequired().IsUnicode(false).HasMaxLength(12);
            builder.Property(x => x.Password).HasColumnName(@"Password").HasColumnType("varchar(12)").IsRequired().IsUnicode(false).HasMaxLength(12);
        }
    }
}
