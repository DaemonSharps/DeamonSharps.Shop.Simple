using DeamonSharps.Shop.Simple.DataBase.Entities;
using System;

namespace DeamonSharps.Shop.Simple.Api.Schemas
{
    [Serializable]
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public static explicit operator Category(Category_DB value)
        {
            return new Category
            {
                Id = value.Id,
                Name = value.Name
            };
        }
    }
}
