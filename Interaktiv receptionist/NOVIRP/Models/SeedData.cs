using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace NOVIRP.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new NOVIRPContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<NOVIRPContext>>()))
            {
                // Look for any NOVI.
                if (context.NOVI.Any())
                {
                    return;   // DB has been seeded
                }

                context.NOVI.AddRange(
                    new NOVI
                    {
                        Navn = "AAU",
                        CVR = 12345678,
                        Medarbejdere = 22,
                        Lokale = "S-A1",
                        Findvej = "Gå til venstre"
                    },
                    new NOVI
                    {
                        Navn = "Nintendo",
                        CVR = 12345678,
                        Medarbejdere = 22,
                        Lokale = "1-A1",
                        Findvej = "Gå til højre"
                    },
                    new NOVI
                    {
                        Navn = "Disney",
                        CVR = 12345678,
                        Medarbejdere = 22,
                        Lokale = "3-A1",
                        Findvej = "He dead,"
                    },
                    new NOVI
                    {
                        Navn = "NOVI",
                        CVR = 12345678,
                        Medarbejdere = 700,
                        Lokale = "S-C3",
                        Findvej = "Ved indgangen"
                    }





                );
                context.SaveChanges();
            }
        }
    }
}
