using Microsoft.Extensions.Caching.Memory;

namespace Product.Infrastructure.CachedData
{
    public abstract class BaseCache<T>: BaseCash
    {
        protected readonly T Repository;
        public BaseCache(IMemoryCache cache, T repository):base(cache) {
            Repository = repository;
        }

    }

    public abstract class BaseCash
    {
        protected readonly IMemoryCache Cache;
        public BaseCash(IMemoryCache cache)
        {
            Cache = cache;
        }

    }
}
