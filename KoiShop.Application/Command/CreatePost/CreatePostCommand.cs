using KoiShop.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Command.CreatePost
{
    public class CreatePostCommand : IRequest<Post>
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? UserId { get; set; } 
        public string? TypePost { get; set; }
    }
}
