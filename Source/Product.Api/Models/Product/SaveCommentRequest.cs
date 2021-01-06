using System.Collections.Generic;

namespace Product.Api.Models.Product
{
    public class SaveCommentRequest
    {
        public string ProductCode { get; set; }
        public string PosterName { get; set; }
        public string Description { get; set; }
        public byte? Rating { get; set; }
    }
}
