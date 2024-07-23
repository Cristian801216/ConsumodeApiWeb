using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ConsumodeApi.Models;

namespace ConsumodeApi.Data
{
    public class ConsumodeApiContext : DbContext
    {
        public ConsumodeApiContext (DbContextOptions<ConsumodeApiContext> options)
            : base(options)
        {
        }

        public DbSet<ConsumodeApi.Models.Lotes> Lotes { get; set; } = default!;
        public DbSet<ConsumodeApi.Models.Grupos> Grupos { get; set; } = default!;
    }
}
