using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JurysBarManagementSystem.Models
{
    public class Customer
    {
        public string? Name { get; set; }
        public string? Contact { get; set; }
        public int LoyaltyPoints { get; set; }
    }
}