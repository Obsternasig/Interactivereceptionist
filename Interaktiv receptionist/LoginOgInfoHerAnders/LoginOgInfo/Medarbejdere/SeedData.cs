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
                        Navn = "Jens",
                        Virksomhed = "Novi",
                        Lokale = "0.12",
                        Email = "Hans@Nu",
                        Stilling = "Chefen"
                    },

                    new MedarbejderInfo
                    {
                        Navn = "Jens Hansen",
                        Virksomhed = "Novi",
                        Lokale = "0.22",
                        Email = "Hans@Nunu",
                        Stilling = "Chefeen"
                    },

                    new MedarbejderInfo
                    {
                        Navn = "Jens Hanseen",
                        Virksomhed = "Novi",
                        Lokale = "0.23",
                        Email = "Hans@Nununu",
                        Stilling = "Chefeeen"
                    },

                    new MedarbejderInfo
                    {
                        Navn = "Jeens Hanseeen",
                        Virksomhed = "Novi",
                        Lokale = "0.24",
                        Email = "Hans@Denbedste",
                        Stilling = "MaxiChefen"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
