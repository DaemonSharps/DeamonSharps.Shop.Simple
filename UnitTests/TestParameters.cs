using System;
using System.Collections.Generic;
using System.Text;

namespace DaemonSharps.Shop.UnitTests
{
    public struct TestParameters
    {
        public static string GetConnectionString(string userName, string password, string dbName = null) 
            => $"server=remotemysql.com;user={userName};password={password};database={dbName ?? userName};";
    }
}
