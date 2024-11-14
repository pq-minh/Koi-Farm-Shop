using KoiShop.Domain.Entities;
using KoiShop.Domain.Respositories;
using KoiShop.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Infrastructure.Respositories
{
    public class PostRepository(KoiShopV1DbContext koiShopV1DbContext) : IPostRepository
    {
        public async Task<Post> CreatePost(Post postDto)
        {
            var post = new Post
            {
                Title = postDto.Title,
                Content = postDto.Content,
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
                Status = "Active",
                TypePost = postDto.TypePost,
                UserId = postDto.UserId
            };
            koiShopV1DbContext.Posts.Add(post);
            await koiShopV1DbContext.SaveChangesAsync();
            return post;
        }
    }
}
