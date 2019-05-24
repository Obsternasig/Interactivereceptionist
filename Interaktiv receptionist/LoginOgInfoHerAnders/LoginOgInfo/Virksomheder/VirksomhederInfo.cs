using System;
using System.ComponentModel.DataAnnotations;

namespace LoginOgInfo.Virksomheder
{
    public class VirksomhederInfo
    {
        public int ID { get; set; }

        [StringLength(30, MinimumLength = 1)]
        [Required]
        public string Navn { get; set; }

        [StringLength(8, MinimumLength = 8)]
        [Required]
        public string CVR { get; set; }

        [StringLength(999, MinimumLength = 1)]
        [Required]
        public string Medarbejdere { get; set; }

        [StringLength(6, MinimumLength = 1)]
        [Required]
        public string Lokale { get; set; }

        [StringLength(1000, MinimumLength = 1)]
        [Required]
        public string Findvej { get; set; }
    }
}
