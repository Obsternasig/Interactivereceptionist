using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NOVIRP.Models;

namespace NOVIRP.Models
{
    public class NOVIRPContext : DbContext
    {
        public NOVIRPContext (DbContextOptions<NOVIRPContext> options)
            : base(options)
        {
        }

        public DbSet<NOVIRP.Models.NOVI> NOVI { get; set; }
    }
}
