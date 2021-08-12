using DeamonSharps.Shop.Simple.Entities;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace DaemonSharps.Shop.UnitTests.Mocks
{
    public class SessionMock: Mock<ISession>
    {
        private readonly Cart _cart = new Cart();

        public SessionMock()
        {
            _cart.Products
                .AddRange(new[] 
                {
                    new CartItem
                    {
                        ProductId = 1,
                        Count = 5
                    }
                });
            Set();
        }

        private void Set()
        {
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, _cart);
                var bytes = stream.ToArray();
                Setup(s => s.TryGetValue(It.Is<string>(s => s == "Cart"), out bytes)).Returns(true);
            }

            
        }
    }
}
