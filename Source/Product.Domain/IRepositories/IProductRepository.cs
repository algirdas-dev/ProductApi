using Product.Domain.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Domain.IRepositories
{
    public interface IProductRepository
    {
        Task<List<ProductDto>> GetProducts();
        Task<List<CommentDto>> GetComments();
        Task SaveComment(CommentDto comment);
    }
}
