using System;

namespace DeamonSharps.Shop.Simple.Api.Schemas
{
    [Serializable]
    public class Order
    {
        public int Id { get; set; }

        public User User { get; set; }

        public DateTime Creation_Date { get; set; }

        public string Status { get; set; }

    }
}
