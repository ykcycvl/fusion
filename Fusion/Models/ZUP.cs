using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusion.Models
{
    public class ZUP
    {
        public class Detention
        {
            public string code { get; set; }
            public string Comment { get; set; }
            public string DateStart { get; set; }
            public string DateEnd { get; set; }
            public string Summ { get; set; }
        }

        public class Employee
        {
            public string code { get; set; }

        }
    }
}