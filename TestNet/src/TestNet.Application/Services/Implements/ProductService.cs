using Microsoft.Extensions.Caching.Memory;
using TestNet.Application.Services.Interfaces;
using TestNet.Core.Repositories;

namespace TestNet.Application.Services.Implements
{
    public class ProductService : IProductService
    {
        private readonly IDapperUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;
        public ProductService(IDapperUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public Dictionary<int, string> GetProductStates()
        {
            var cacheKey = "ProductStates";

            if (_cache.TryGetValue(cacheKey, out Dictionary<int, string> cachedProductStates))
            {
                return cachedProductStates;
            }
            else
            {
                var productStates = LoadProductStates();

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                };

                _cache.Set(cacheKey, productStates, cacheEntryOptions);

                return productStates;
            }
        }

        private Dictionary<int, string> LoadProductStates()
        {
            return new Dictionary<int, string>
            {
                { 0, "Inactive" },
                { 1, "Active" }
            };
        }
    }
}
