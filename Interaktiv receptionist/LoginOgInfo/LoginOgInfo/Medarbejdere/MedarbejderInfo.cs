using System;
using System.ComponentModel.DataAnnotations;

namespace LoginOgInfo.Medarbejdere
{
    public class MedarbejderInfo
    {
        public int ID { get; set; }
        public string Navn { get; set; }
        public string Virksomhed { get; set; }
        public string Lokale { get; set; }
        public string Email { get; set; }
        public string Stilling { get; set; }
    }
}
