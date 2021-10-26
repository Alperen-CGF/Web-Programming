using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class Film

    {
        public int Film_ID { get; set; }
        public string Film_Name { get; set; }
        public string Film_Pic { get; set; }
        public string Explan { get; set; }
        public DateTime Vision_Date { get; set; }
        public Decimal Imdb { get; set; }
        public int Time { get; set; }
    }
}