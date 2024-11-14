using KoiShop.Application.Dtos;
using KoiShop.Application.Dtos.BlogDtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Interfaces
{
    public interface IBlogService
    {
        Task<bool> CreateBlogPost(AddBlogDto blogPost);
        Task<bool> UpdateBlog(UpdateBlogDto blogPost);
        Task<List<AddBlogDto>> GetAllBlogs();
        Task<AddBlogDto> GetBlogById(string blogId);

    }
}
