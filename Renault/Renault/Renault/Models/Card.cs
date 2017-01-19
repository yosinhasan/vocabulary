using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renault.Models
{
    public class Card
    {
        public string license { get; set; }
        public string cardNumber { get; set; }
        public string endMileage { get; set; }
        public string expireDate { get; set; }
        public string fullName { get; set; }
        public string dealerName { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string postCode { get; set; }
        public string placeName { get; set; }

        public override string ToString()
        {
            return license + " " + cardNumber;
        }
    }
}
