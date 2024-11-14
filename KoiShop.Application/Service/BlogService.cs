using KoiShop.Application.Dtos;
using KoiShop.Application.Dtos.BlogDtos;
using KoiShop.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Service
{
    public class BlogService : IBlogService
    {
        private readonly FirebaseService _firebaseService;
        private const string CollectionName = "Blogs";

        public BlogService(FirebaseService firebaseService)
        {
            _firebaseService = firebaseService;
        }

        public async Task<bool> CreateBlogPost(AddBlogDto blogPost)
        {
            if (blogPost == null)
            {
                throw new ArgumentNullException(nameof(blogPost), "Blog post is null.");
            }
            var result = await _firebaseService.SaveDocument(blogPost, CollectionName);
            return result != null;
        }

        public async Task<bool> UpdateBlog(UpdateBlogDto blogPost)
        {
            if (blogPost == null)
            {
                throw new ArgumentNullException(nameof(blogPost), "Blog post is null.");
            }

            try
            {
                var currentBlog = await _firebaseService.GetDocumentById<AddBlogDto>(blogPost.id, "Blogs");

                if (currentBlog == null)
                {
                    return false;
                }

                if (!string.IsNullOrWhiteSpace(blogPost.title))
                {
                    currentBlog.title = blogPost.title;
                }

                if (!string.IsNullOrWhiteSpace(blogPost.content))
                {
                    currentBlog.content = blogPost.content;
                }

                currentBlog.updateDate = DateTime.UtcNow;

                if (!string.IsNullOrWhiteSpace(blogPost.status))
                {
                    currentBlog.status = blogPost.status;
                }

                if (!string.IsNullOrWhiteSpace(blogPost.blogType))
                {
                    currentBlog.blogType = blogPost.blogType;
                }

                if (!string.IsNullOrWhiteSpace(blogPost.userId))
                {
                    currentBlog.userId = blogPost.userId;
                }

                await _firebaseService.UpdateDocument(currentBlog.id, currentBlog, "Blogs");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<List<AddBlogDto>> GetAllBlogs()
        {
            return await _firebaseService.GetDocuments<AddBlogDto>(CollectionName);
        }

        public async Task<AddBlogDto> GetBlogById(string blogId)
        {
            return await _firebaseService.GetDocumentById<AddBlogDto>(blogId, CollectionName);
        }
    }
}
