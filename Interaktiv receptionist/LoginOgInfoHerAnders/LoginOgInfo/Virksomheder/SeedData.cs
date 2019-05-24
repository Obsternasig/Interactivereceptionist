using LoginOgInfo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace LoginOgInfo.Virksomheder
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LoginOgInfoContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<LoginOgInfoContext>>()))
            {
                
                if (context.VirksomhederInfo.Any())
                {
                    return;   // DB has been seeded
                }

                context.VirksomhederInfo.AddRange(
                    new VirksomhederInfo
                    {
                        Navn = "Novi101",
                        CVR = "66554466",
                        Medarbejdere = "44",
                        Lokale = "0.01",
                        Findvej = "Gå mod udgangen og drej til højre før du går ud"
                    },

                    new VirksomhederInfo
                    {
                        Navn = "Novi9",
                        CVR = "22114433",
                        Medarbejdere = "565",
                        Lokale = "0.44",
                        Findvej = "Gå op af træpperne og til venstre"
                    },

                   new VirksomhederInfo
                   {
                       Navn = "Novi8",
                       CVR = "11223344",
                       Medarbejdere = "42",
                       Lokale = "0.32",
                       Findvej = "Gå til højre"
                   },

                    new VirksomhederInfo
                    {
                        Navn = "Novi",
                        CVR = "99887766",
                        Medarbejdere = "333",
                        Lokale = "0.33",
                        Findvej = "Gå til venstre"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}