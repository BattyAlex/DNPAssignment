using DataTransferObjects;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;


namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController
{
    private readonly IPostRepository postRepository;
    private readonly IUserRepository userRepository;
    private readonly ICommentRepository commentRepository;

    public PostsController(IPostRepository postRepository, IUserRepository userRepository, ICommentRepository commentRepository)
    {
        this.postRepository = postRepository;
        this.userRepository = userRepository;
        this.commentRepository = commentRepository;
    }

    [HttpPost]
    public async Task<IResult> CreatePost([FromBody] CreatePostDTO post)
    {
        User author = GetUserByName(post.Author);
        if (author == null)
        {
            return Results.NotFound("User does not exist");
        }
        else
        {
            Post newPost = new Post()
                    {
                        Title = post.Title,
                        Content = post.Content,
                        UserID = author.Id
                    };
                    await postRepository.AddPostAsync(newPost);
                    return Results.Created($"/Posts/{newPost.ID}", newPost);
        }
    }

    [HttpPut]
    public async Task<IResult> UpdatePost([FromRoute] int id, [FromBody] ReplacePostDTO request)
    {
        try
        {
            Post post = postRepository.GetSinglePostAsync(id).Result;
            post.Content = request.Content;
            post.Title = request.Title;
            await postRepository.UpdatePostAsync(post);
            return Results.Created($"/Posts/{post.ID}", post);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpDelete]
    public async Task<IResult> DeletePost([FromRoute] int id)
    {
        await postRepository.DeletePostAsync(id);
        return Results.NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetSinglePost([FromRoute] int id, [FromQuery] bool includeAuthor, [FromQuery] bool includeComments) 
    {
        try
        {
            Post post = await postRepository.GetSinglePostAsync(id);
            CompletePostDTO result = new()
            {
                Title = post.Title,
                Content = post.Content,
            };
            if (includeAuthor)
            {
                User author = await userRepository.GetSingleUserAsync(post.UserID);
                result.Author = author.Name;
            }

            if (includeComments)
            {
                List<CommentDTO> commentsForPost = new List<CommentDTO>();
                List<Comment> comments = commentRepository.GetAll().ToList();
                foreach (Comment comment in comments)
                {
                    if (comment.PostId == id)
                    {
                        User commenter = userRepository.GetSingleUserAsync(comment.UserId).Result;
                        CommentDTO com = new()
                        {
                            CommentBody = comment.CommentBody,
                            Commenter = commenter.Name
                        };
                        commentsForPost.Add(com);
                    }
                }
                result.Comments = commentsForPost;
            }
            return Results.Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Results.NotFound(e.Message);
        }
    }

    [HttpGet]
        public IResult GetManyPosts([FromQuery] string? nameContains, [FromQuery] int? writtenBy)
        {
            List<Post> posts = postRepository.GetMultiplePosts().ToList();
            if (!string.IsNullOrWhiteSpace(nameContains))
            {
                posts = posts.Where(p => p.Title.ToLower().Contains(nameContains.ToLower())).ToList();
            }
            if (writtenBy.HasValue)
            {
                posts = posts.Where(p => p.UserID == writtenBy).ToList();
            }
            return Results.Ok(posts);
        }

    private User GetUserByName(string userName)
    {
        List<User> users = userRepository.GetManyUsersAsync().ToList();
        foreach (User user in users)
        {
            if (user.Name == userName)
            {
                return user;
            }
        }
        return null;
    }
}
