using LoginOgInfo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace LoginOgInfo.Medarbejdere
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LoginOgInfoContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<LoginOgInfoContext>>()))
            {
                
                if (context.MedarbejderInfo.Any())
                {
                    return;   // DB has been seeded
                }

                context.MedarbejderInfo.AddRange(
                    new MedarbejderInfo
                    {
                        Navn = "Jens Hansen",
                        Virksomhed = "Novi",
                        Lokale = "0.12.1",
                        Email = "Hans@Nu.dk",
                        Stilling = "Gå til højre"
                    },

                    new MedarbejderInfo
                    {
                        Navn = "Jens Karlsen",
                        Virksomhed = "Hjem-Is",
                        Lokale = "0.22",
                        Email = "Karl@Hjemis.dk",
                        Stilling = "Første dør på venstre hånd"
                    },

                    new MedarbejderInfo
                    {
                        Navn = "Jens Jensen",
                        Virksomhed = "Stofa",
                        Lokale = "S-A1",
                        Email = "jens12@stofa.dk",
                        Stilling = "Gå ned af gangen og tag første dør på venstre hånd"
                    },

                    new MedarbejderInfo
                    {
                        Navn = "Karl Jensen",
                        Virksomhed = "Mozilla",
                        Lokale = "0.24",
                        Email = "Hans@Mozilla.dk",
                        Stilling = "Gå ned af gangen og tag anden dør på højre hånd"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
