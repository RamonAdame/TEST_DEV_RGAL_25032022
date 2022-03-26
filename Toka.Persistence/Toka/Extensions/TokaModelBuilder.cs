using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Toka.Persistence.Toka.Configurations;

namespace Toka.Persistence.Toka.Extensions
{
    public static class TokaModelBuilder
    {
        public static void ApplyTokaConfiguration(this TokaContext tokaContext, ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Tb_PersonasFisicasConfiguration());
            modelBuilder.ApplyConfiguration(new Tb_LoginConfiguration());
        }
    }
}
