using System;

namespace DeamonSharps.Shop.Simple.Api.Schemas
{
    [Serializable]
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public Category Category { get; set; }

    }
}
