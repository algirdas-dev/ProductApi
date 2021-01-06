using Dapper;
using Product.DB;
using Product.Domain.Dtos;
using Product.Domain.IRepositories;
using Product.Helpers.Connections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
         
        public ProductRepository(IDatabaseConnectionFactory connectionFactory, ProductContext context) :base(connectionFactory: connectionFactory, context:context) {
        }

        public async Task<List<ProductDto>> GetProducts() {
            using (var connection = await ConnectionFactory.CreateConnectionAsync().ConfigureAwait(false)) {
                string query = "select ProductId, Name, Code, Description from Products where IsDeleted = 0 and IsEnabled = 1";
                var result = await connection.QueryAsync<ProductDto>(query).ConfigureAwait(false);
                return result.ToList();
            }
        }

        public async Task<List<CommentDto>> GetComments()
        {
            using (var connection = await ConnectionFactory.CreateConnectionAsync().ConfigureAwait(false))
            {
                string query = @"select c.CommentId, c.ProductId, p.Name as ProductName, c.PosterName, c.Description, c.Rating
                    from Products p
                    join Comments c on p.ProductId = c.ProductId
                    where c.IsDeleted = 0 and c.IsEnabled = 1 and p.IsDeleted = 0 and p.IsEnabled = 1";
                var result = await connection.QueryAsync<CommentDto>(query).ConfigureAwait(false);
                return result.ToList();
            }
        }

        public async Task SaveComment(CommentDto comment)
        {
            Context.Comments.Add(new DB.Models.Comment
            {
                ProductId = comment.ProductId.Value,
                PosterName = comment.PosterName,
                Description = comment.Description,
                Rating = comment.Rating
            });

            await Context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
