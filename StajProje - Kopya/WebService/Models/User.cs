using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class User
    {
        public int User_ID { get; set; }
        public string UserName { get; set; }
        public long Password { get; set; }
        public int Birth_Date { get; set; }
        public int Telefon { get; set; }
    }
}