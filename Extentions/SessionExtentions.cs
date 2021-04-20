using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace DeamonSharps.Shop.Simple.Extentions
{
    public static class SessionExtentions
    {
        private static readonly IFormatter _formatter = new BinaryFormatter();

        /// <summary>
        /// Получает объект из сессии по ключу
        /// </summary>
        public static T Get<T>(this ISession session,string key) where T : class
        {
            if (session.TryGetValue(key, out byte[] data)==true)
            {
                using (var stream=new MemoryStream(data))
                {
                  return  _formatter.Deserialize(stream) as T;
                }
                    
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Сериализует объект в сессию по ключу
        /// </summary>
        public static void Set(this ISession session, string key, object data) 
        {
            using (var stream= new MemoryStream())
            {
                _formatter.Serialize(stream,data);
                session.Set(key,stream.ToArray());
            }
        }
    }
}
