using DeamonSharps.Shop.Simple.DataBase.Context;
using DeamonSharps.Shop.Simple.DataBase.Entities;
using DeamonSharps.Shop.Simple.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DaemonSharps.Shop.UnitTests
{
    /// <summary>
    /// Универсальный базовый класс для тестов с базой данных
    /// </summary>
    /// <typeparam name="T">Класс контекста БД</typeparam>
    public class DBTestBase<T> where T: DbContext, IDefaultValue<List<object>>
    {
        public static Type TestClassType = null;
        protected DBTestBase(DbContextOptions<T> options, Type testClass)
        {
            ContextOptions = options;

            //Блок нужен, для того, чтобы с БД одновременно работал только один тестовый класс
            do
            {
                if (TestClassType == null)
                {
                    TestClassType = testClass;
                }
                else if (TestClassType != testClass)
                {
                    Thread.Sleep(100);
                }
            } while (TestClassType != testClass);
            
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

        protected async Task WithDBContextAsync(Func<T,Task> action)
        {
            using (var context = (T)Activator.CreateInstance(typeof(T), ContextOptions))
            {
                await action(context);

                TestClassType = null;
            }
        }
    }
}
