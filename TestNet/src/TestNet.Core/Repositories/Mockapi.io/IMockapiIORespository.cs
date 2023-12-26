using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNet.Core.Repositories.Mockapi.io
{
    public interface IMockapiIORespository
    {
        GetDiscountResponse? GetDiscount(long productId);
    }
}
