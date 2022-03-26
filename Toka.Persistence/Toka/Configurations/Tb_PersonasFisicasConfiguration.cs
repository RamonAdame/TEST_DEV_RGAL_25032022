using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toka.Domain.Models;

namespace Toka.Persistence.Toka.Configurations
{
    public class Tb_PersonasFisicasConfiguration : IEntityTypeConfiguration<Tb_PersonasFisicas>
    {
        public void Configure(EntityTypeBuilder<Tb_PersonasFisicas> builder)
        {
            builder.ToTable("Tb_PersonasFisicas");
            builder.HasKey(x => x.IdPersonaFisica).HasName("PK_Tb_PersonasFisicas").IsClustered();

            builder.Property(x => x.IdPersonaFisica).HasColumnName(@"IdPersonaFisica").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(x => x.FechaRegistro).HasColumnName(@"FechaRegistro").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.FechaActualizacion).HasColumnName(@"FechaActualizacion").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.Nombre).HasColumnName(@"Nombre").HasColumnType("varchar(50)").IsRequired(false).IsUnicode(false).HasMaxLength(50);
            builder.Property(x => x.ApellidoPaterno).HasColumnName(@"ApellidoPaterno").HasColumnType("varchar(50)").IsRequired(false).IsUnicode(false).HasMaxLength(50);
            builder.Property(x => x.ApellidoMaterno).HasColumnName(@"ApellidoMaterno").HasColumnType("varchar(50)").IsRequired(false).IsUnicode(false).HasMaxLength(50);
            builder.Property(x => x.RFC).HasColumnName(@"RFC").HasColumnType("varchar(13)").IsRequired(false).IsUnicode(false).HasMaxLength(13);
            builder.Property(x => x.FechaNacimiento).HasColumnName(@"FechaNacimiento").HasColumnType("date");
            builder.Property(x => x.UsuarioAgrega).HasColumnName(@"UsuarioAgrega").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.Activo).HasColumnName(@"Activo").HasColumnType("bit").IsRequired(false);
        }
    }
}
