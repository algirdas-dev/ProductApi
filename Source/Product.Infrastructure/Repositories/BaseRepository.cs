using Product.DB;
using Product.Helpers.Connections;

namespace Product.Infrastructure.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly IDatabaseConnectionFactory ConnectionFactory;
        protected readonly ProductContext Context;
        public BaseRepository(IDatabaseConnectionFactory connectionFactory = null, ProductContext context = null) {
            ConnectionFactory = connectionFactory;
            Context = context;
        }
    }
}
