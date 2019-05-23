using System;
using System.ComponentModel.DataAnnotations;

namespace LoginOgInfo.Lokaler
{
    public class LokalerInfo
    {
        public int ID { get; set; }
        public string Lokalenummer { get; set; }
        public string Lejer { get; set; }
        public string Medarbejderantal { get; set; }
    }
}
