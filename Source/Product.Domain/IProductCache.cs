using Product.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain
{
    public interface IProductCache
    {
        Task<List<ProductDto>> List();
        Task<List<CommentDto>> Comments(int programId);
    }
}
