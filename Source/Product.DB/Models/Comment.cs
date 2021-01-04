using System;
using System.Collections.Generic;
using System.Text;

namespace Product.DB.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int ProductId { get; set; }
        public string PosterName { get; set; }
        public string Description { get; set; }
        public byte? Rating { get; set; }

        public Product Product { get; set; }
    }
}
