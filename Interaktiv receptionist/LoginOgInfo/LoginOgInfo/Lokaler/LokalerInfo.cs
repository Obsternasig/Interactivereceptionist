using System;
using System.ComponentModel.DataAnnotations;

namespace LoginOgInfo.Lokaler
{
    public class LokalerInfo
    {
        public int ID { get; set; }

        [StringLength(6, MinimumLength = 1)]
        [Required]
        public string Lokalenummer { get; set; }
        [StringLength(60, MinimumLength = 1)]
        [Required]
        public string Lejer { get; set; }
        [Range(1, 999)]
        [Required]
        public string Medarbejderantal { get; set; }
    }
}
