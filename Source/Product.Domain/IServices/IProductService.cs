using Product.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.IServices
{
    public interface IProductService
    {
        Task<ProductDto> Single(string name, bool? bestRatingProduct, bool? worstRatingProduct);
        Task<List<CommentDto>> GetComments(int? productId);
        Task<int?> GetId(string code);
        Task SaveComment(CommentDto comment);
    }
}
