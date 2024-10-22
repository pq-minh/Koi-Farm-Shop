using KoiShop.Application.Dtos;
using KoiShop.Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace KoiShop.Controllers
{
    [ApiController]
    [Route("api/blog/management")]
    public class BlogController : ControllerBase
    {
        private readonly FirebaseService _firebaseService;

        public BlogController(FirebaseService firebaseService)
        {
            _firebaseService = firebaseService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody] BlogDto blogPost)
        {
            if (blogPost == null)
            {
                return BadRequest("Blog post is null.");
            }

            string postId = await _firebaseService.SaveBlogPost(blogPost);
            return CreatedAtAction(nameof(CreateBlogPost), new { id = postId }, blogPost);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllBlogs()
        {
            try
            {
                var blogs = await _firebaseService.GetBlogPosts();
                return Ok(blogs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving blogs.");
            }
        }
    }
}
