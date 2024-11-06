using KoiShop.Domain.Entities;
using KoiShop.Domain.Respositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KoiShop.Application.Users.UserContext;

namespace KoiShop.Application.Command.CreatePost
{
    public class CreatePostCommanHandle(IUserContext userContext,IPostRepository postRepository) : IRequestHandler<CreatePostCommand, Post>
    {
        public Task<Post> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {

            var user = userContext.GetCurrentUser();
            var post = new Post
            {
                Title = request.Title,
                Content = request.Content,
                TypePost = request.TypePost,
                UserId = user.Id,
            };
            var result = postRepository.CreatePost(post);
            return result;
        }
    }
}
