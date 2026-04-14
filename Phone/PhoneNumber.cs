using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegister
{
    public class PhoneNumber
    {
        public string Number { get; set; }
        public string Category { get; set; } 

        public PhoneNumber(string number, string category)
        {
            Number = number;
            Category = category;
        }

        public override string ToString()
        {
            return $"{Category}: {Number}";
        }
    }

}
