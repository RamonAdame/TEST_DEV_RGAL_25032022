using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Toka.Domain.DTO;
using Toka.Domain.Models;
using Toka.Persistence.Toka.Extensions;

namespace Toka.Persistence
{
    public class TokaContext : DbContext
    {
        public TokaContext()
        {

        }

        public TokaContext(DbContextOptions<TokaContext> options) : base(options) { }

        //DBSET PARA QUE LA TABLA EXISTA EN EL DBCONTEXT

        public DbSet<Tb_PersonasFisicas> Tb_PersonasFisicas { get; set; } // Tb_PersonasFisicas
        public DbSet<Tb_Login> Tb_Login { get; set; } // Tb_Login

        #region Entity Configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            this.ApplyTokaConfiguration(modelBuilder);
        }
        #endregion
    }
}
