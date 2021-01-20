using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Domain.Dtos
{
    public class CommentDto : IComparable<CommentDto>
    {
        public int CommentId { get; set; }
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public string PosterName { get; set; }
        public string Description { get; set; }
        public byte? Rating { get; set; }

        public int CompareTo(CommentDto that)
        {
            return 1;
        }
    }
}
