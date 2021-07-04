using System;

namespace DeamonSharps.Shop.Simple.Api.Schemas
{
    [Serializable]
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public Role Role { get; set; }

        public string Email_Adress { get; set; }

        public string Password { get; set; }
    }
}
