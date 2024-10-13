using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Domain.Entities
{
    public class Post
    {
        public int PostID { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public string? Status { get; set; }
        public string? TypePost { get; set; }

        public string? UserId { get; set; }
        public virtual User User { get; set; }
    }
}
