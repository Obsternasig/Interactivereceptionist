using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LoginOgInfo.Lokaler;
using LoginOgInfo.Medarbejdere;
using LoginOgInfo.Virksomheder;

namespace LoginOgInfo.Models
{
    public class LoginOgInfoContext : DbContext
    {
        public LoginOgInfoContext (DbContextOptions<LoginOgInfoContext> options)
            : base(options)
        {
        }

        public DbSet<LoginOgInfo.Lokaler.LokalerInfo> LokalerInfo { get; set; }

        public DbSet<LoginOgInfo.Medarbejdere.MedarbejderInfo> MedarbejderInfo { get; set; }

        public DbSet<LoginOgInfo.Virksomheder.VirksomhederInfo> VirksomhederInfo { get; set; }
    }
}
