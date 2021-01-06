using Product.Domain;
using Product.Domain.Constants;
using Product.Domain.Dtos;
using Product.Domain.IHelpers.IQueueings;
using Product.Domain.IRepositories;
using Product.Domain.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Infrastructure.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductCache _cache;
        private readonly IProductRepository _repository;
        public ProductService(IProductCache cache, IProductRepository repository, IMsmqHelper msmq):base(msmq) {
            _cache = cache;
            _repository = repository;
        }

        public async Task<ProductDto> Single(string name, bool? bestRatingProduct, bool? worstRatingProduct) {
            var products = await _cache.List().ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(name)) 
                return products.FirstOrDefault(p => p.Name.ToLower() == name.ToLower());
            else if (bestRatingProduct.HasValue && bestRatingProduct.Value)
                return products.OrderBy(async p => (await _cache.Comments(p.ProductId))?.Average(c=>c.Rating)).FirstOrDefault(p => p.Name.ToLower() == name.ToLower());
            else if (worstRatingProduct.HasValue && worstRatingProduct.Value)
                return products.OrderByDescending(async p => (await _cache.Comments(p.ProductId))?.Average(c => c.Rating)).FirstOrDefault(p => p.Name.ToLower() == name.ToLower());


            return null;
        }

        public async Task<int?> GetId(string code) {
            var products = await _cache.List().ConfigureAwait(false);
            if (!string.IsNullOrWhiteSpace(code))
                return products.FirstOrDefault(p => p.Code.ToLower() == code.ToLower())?.ProductId;

            return null;
        }


        public async Task<List<CommentDto>> GetComments(int? productId)
        {
            if (!productId.HasValue)
                return null;

            return await _cache.Comments(productId.Value).ConfigureAwait(false);
        }

        public async Task SaveComment(CommentDto comment)
        {
            if (!comment.ProductId.HasValue)
                throw new ArgumentNullException(nameof(comment.ProductId), "SaveComment service exeption");
            if (string.IsNullOrWhiteSpace(comment.PosterName))
                throw new ArgumentNullException(nameof(comment.PosterName), "SaveComment service exeption");
            if (string.IsNullOrWhiteSpace(comment.Description))
                throw new ArgumentNullException(nameof(comment.Description), "SaveComment service exeption");

            try
            {
                await _repository.SaveComment(comment).ConfigureAwait(false);
            }
            catch (Exception ex) {
                Msmq.Save(MsmqConst.NotSavedComments, comment);
                throw new Exception(ex.Message ,ex);
            }
        }
    }
}
