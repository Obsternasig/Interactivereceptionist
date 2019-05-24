using System;
using System.ComponentModel.DataAnnotations;

namespace LoginOgInfo.BrugerParkering
{
    public class ParkeringsInfo
    {
        public int ID { get; set; }
        [StringLength(7, MinimumLength = 2)]
        [Required]
        public string Nummerplade { get; set; } 
    }
}
