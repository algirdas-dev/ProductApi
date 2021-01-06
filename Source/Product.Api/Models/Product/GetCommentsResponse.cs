using System.Collections.Generic;

namespace Product.Api.Models.Product
{
    public class GetCommentResponse
    {
        public List<Comment> Comments { get; set; }

        public class Comment {
            public string ProductName { get; set; }
            public string PosterName { get; set; }
            public string Description { get; set; }
            public byte? Rating { get; set; }
        }
    }
}
