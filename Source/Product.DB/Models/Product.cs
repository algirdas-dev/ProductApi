using System.Collections.Generic;

namespace Product.DB.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
