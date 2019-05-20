using System;
using System.ComponentModel.DataAnnotations;

namespace NOVIRP.Models
{
    public class NOVI
    {
        public int ID { get; set; }
        public string Navn { get; set; }

        public int CVR { get; set; }
        public int Medarbejdere { get; set; }
        public string Lokale { get; set; }
        public string Findvej { get; set; }

    }
}