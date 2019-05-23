using System;
using System.ComponentModel.DataAnnotations;

namespace LoginOgInfo.Virksomheder
{
    public class VirksomhederInfo
    {
        public int ID { get; set; }
        public string Navn { get; set; }
        public string CVR { get; set; }
        public string Medarbejdere { get; set; }
        public string Lokale { get; set; }
        public string Findvej { get; set; }
    }
}
