using LoginOgInfo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace LoginOgInfo.Lokaler
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LoginOgInfoContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<LoginOgInfoContext>>()))
            {
                
                if (context.LokalerInfo.Any())
                {
                    return;   // DB has been seeded
                }

                context.LokalerInfo.AddRange(
                    new LokalerInfo
                    {
                        Lokalenummer = "0.21",
                        Lejer = "Novi",
                        Medarbejderantal = "12"
                    },

                    new LokalerInfo
                    {
                        Lokalenummer = "0.22",
                        Lejer = "Novi8",
                        Medarbejderantal = "34"
                    },

                    new LokalerInfo
                    {
                        Lokalenummer = "0.23",
                        Lejer = "Novi9",
                        Medarbejderantal = "666"
                    },

                    new LokalerInfo
                    {
                        Lokalenummer = "",                        
                        Lejer = "",
                        Medarbejderantal = ""
                    }
                );
                context.SaveChanges();
            }
        }
    }
}