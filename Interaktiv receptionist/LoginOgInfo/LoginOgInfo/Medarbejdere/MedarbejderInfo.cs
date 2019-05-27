using System;
using System.ComponentModel.DataAnnotations;

namespace LoginOgInfo.Medarbejdere
{
    public class MedarbejderInfo
    {
        public int ID { get; set; }

        [StringLength(50, MinimumLength = 1)]
        [Required]
        public string Navn { get; set; }

        [StringLength(60, MinimumLength = 1)]
        [Required]
        public string Virksomhed { get; set; }

        [StringLength(6, MinimumLength = 1)]
        [Required]
        public string Lokale { get; set; }

        //Behøver ikke required da valuetyper er det automatisk.

        [StringLength(60, MinimumLength = 1)]
        [Required]
        public string Email { get; set; }

        [StringLength(200, MinimumLength = 1)]
        [Required]
        public string Stilling { get; set; }
    }
}
