using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNet.Application.Dtos;
using TestNet.Core.Entities;

namespace TestNet.Application.Services.Interfaces
{
    public interface IProductService
    {
        Dictionary<int, string> GetProductStates();
    }
}
