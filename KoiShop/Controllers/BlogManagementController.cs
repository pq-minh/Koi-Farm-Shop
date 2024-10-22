using KoiShop.Application.Dtos.BlogDtos;
using KoiShop.Application.Dtos.KoiShop.Application.Dtos;
using KoiShop.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KoiShop.Controllers
{
    [ApiController]
    [Route("api/blog/management")]
    public class BlogManagementController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogManagementController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody] AddBlogDto blogPost)
        {
            if (blogPost == null)
            {
                return BadRequest("Blog post is null.");
            }

            try
            {
                var create = await _blogService.CreateBlogPost(blogPost);
                if(create)
                    return Ok("Create Blog successfully.");

                return BadRequest("An error occurred while creating the blog post...");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the blog post: {ex.Message}");
            }
        }

        [HttpPut("{blogId}")]
        public async Task<IActionResult> UpdateBlog(string blogId, [FromBody] UpdateBlogDto blogPost)
        {
            if (blogPost == null)
            {
                return BadRequest("Blog post is null.");
            }

            try
            {
                bool updated = await _blogService.UpdateBlog(blogId, blogPost);
                if (updated)
                {
                    return Ok("Blog post updated successfully.");
                }
                return NotFound("Blog post not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the blog post: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogs()
        {
            try
            {
                var blogs = await _blogService.GetAllBlogs();
                return Ok(blogs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving blogs: {ex.Message}");
            }
        }

        [HttpGet("{blogId}")]
        public async Task<IActionResult> GetBlogById(string blogId)
        {
            try
            {
                var blog = await _blogService.GetBlogById(blogId);
                if (blog != null)
                {
                    return Ok(blog);
                }
                else
                {
                    return NotFound($"Blog post with ID {blogId} not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the blog post: {ex.Message}");
            }
        }
    }
}
