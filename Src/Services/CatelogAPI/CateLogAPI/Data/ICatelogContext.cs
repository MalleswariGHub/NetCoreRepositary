using CateLogAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CateLogAPI.Data
{
    public interface ICatelogContext
    {
        ICatelogContext<Product> Products { get; }
    }
}
