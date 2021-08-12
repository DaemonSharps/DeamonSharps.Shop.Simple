using DeamonSharps.Shop.Simple.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DaemonSharps.Shop.UnitTests
{
    /// <summary>
    /// Универсальный базовый класс для тестов с базой данных
    /// </summary>
    /// <typeparam name="T">Класс контекста БД</typeparam>
    public class DBTestBase<T> where T : DbContext, IDefaultValue<List<object>>
    {
        protected DBTestBase(DbContextOptions<T> options)
        {
            ContextOptions = options;

            Seed();
        }

        protected DbContextOptions<T> ContextOptions { get; }

        private void Seed()
        {
            using (var context = (T)Activator.CreateInstance(typeof(T), ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var seedItems = context.GetDefaultValue();

                context.AddRange(seedItems);
                context.SaveChanges();
            }
        }

        protected async Task WithDBContextAsync(Func<T, Task> action)
        {
            using (var context = (T)Activator.CreateInstance(typeof(T), ContextOptions))
            {
                await action(context);
            }
        }
    }
}
