using KoiShop.Application.Dtos.BlogDtos;
using KoiShop.Application.Dtos.KoiShop.Application.Dtos;
using KoiShop.Application.Interfaces;
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

        public async Task<bool> UpdateBlog(string blogId, UpdateBlogDto blogPost)
        {
            if (blogPost == null)
            {
                throw new ArgumentNullException(nameof(blogPost), "Blog post is null.");
            }

            try
            {
                // Lấy đối tượng blog hiện tại từ Firestore theo id
                var currentBlog = await _firebaseService.GetDocumentById<AddBlogDto>(blogId, "Blogs");

                if (currentBlog == null)
                {
                    return false;
                }

                // Kiểm tra và cập nhật các thuộc tính từ UpdateBlogDto
                if (!string.IsNullOrWhiteSpace(blogPost.title))
                {
                    currentBlog.title = blogPost.title;
                }

                if (!string.IsNullOrWhiteSpace(blogPost.content))
                {
                    currentBlog.content = blogPost.content;
                }

                // Cập nhật ngày chỉnh sửa
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

                // Cập nhật document trong Firestore
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
