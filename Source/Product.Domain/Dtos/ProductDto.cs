using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Domain.Dtos
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
