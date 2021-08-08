using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeamonSharps.Shop.Simple.Interfaces
{
    public interface IDefaultValue<T>
    {
        T GetDefaultValue();
    }
}
