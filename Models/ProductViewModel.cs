using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeamonSharps.Shop.Simple.Models
{
    public struct ProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int Count { get; set; }
    }
}
