using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using System;

namespace oeschle.Models
{
    public class Employee
    {
        public long id { get; set; }
        public string name { get; set; }
        public string document_number { get; set; }
        public double salary { get; set; }
        public int age { get; set; }
        public string profile { get; set; }
        public DateTime admission_date { get; set; }

    }
}
