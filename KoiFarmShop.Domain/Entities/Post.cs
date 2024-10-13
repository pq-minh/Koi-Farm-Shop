using System;
using System.Collections.Generic;

namespace KoiFarmShop.Domain.Entities
{

    public partial class Post
    {
        public int PostId { get; set; }

        public string? Title { get; set; }

        public string? Content { get; set; }

        public DateOnly CreateDate { get; set; }

        public DateOnly UpdateDate { get; set; }

        public string? Status { get; set; }

        public string? TypePost { get; set; }

        public string? UserId { get; set; }

        public virtual AspNetUser? User { get; set; }
    }
}