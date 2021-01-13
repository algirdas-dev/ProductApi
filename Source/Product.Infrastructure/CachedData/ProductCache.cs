using Microsoft.Extensions.Caching.Memory;
using Product.Domain;
using Product.Domain.Constants;
using Product.Domain.Dtos;
using Product.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Infrastructure.CachedData
{
    public class ProductCache : BaseCache<IProductRepository>, IProductCache
    {
        public ProductCache(IMemoryCache cache, IProductRepository repository) : base(cache, repository) { }

        public async Task<List<ProductDto>> List()
        {
            var products = await
            Cache.GetOrCreateAsync<List<ProductDto>>(CacheConst.Products, async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromHours(1);
                return await Repository.GetProducts().ConfigureAwait(false);
            });
            return products;
        }

        public async Task<List<CommentDto>> Comments(int programId)
        {
            var productsComments = await
            Cache.GetOrCreateAsync<Dictionary<int, List<CommentDto>>>(CacheConst.Comments, async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromHours(1);
                var comments = await Repository.GetComments().ConfigureAwait(false);
                var result = new Dictionary<int, List<CommentDto>>();
                foreach (var comment in comments)
                {
                    if (!result.ContainsKey(comment.ProductId.Value))
                        result.Add(comment.ProductId.Value, new List<CommentDto>());

                    result[comment.ProductId.Value].Add(comment);
                }

                return result;
            });

            var test = productsComments.TryGetValue(programId, out var values2) ? values2 : null;

            return productsComments.TryGetValue(programId, out var values) ? values : null;

        }
    }
}
