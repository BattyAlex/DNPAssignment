using DataTransferObjects;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;


namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController
{
    private readonly IPostRepository postRepository;
    private readonly IUserRepository userRepository;
    private readonly ICommentRepository commentRepository;

    public PostsController(IPostRepository postRepository,
        IUserRepository userRepository, ICommentRepository commentRepository)
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
            Post newPost = await postRepository.AddPostAsync(new()
            {
                Title = post.Title,
                Content = post.Content,
                UserID = author.Id
            });
            CompletePostDTO created = new()
            {
                Id = newPost.ID,
                Title = newPost.Title,
                Content = newPost.Content,
                Author = userRepository.GetSingleUserAsync(newPost.UserID).Result.Name
            };
            return Results.Created($"/Posts/{newPost.ID}", created);
        }
    }

    [HttpPut]
    public async Task<IResult> UpdatePost([FromRoute] int id,
        [FromBody] ReplacePostDTO request)
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

    [HttpDelete("{id}")]
    public async Task<IResult> DeletePost([FromRoute] int id)
    {
        await postRepository.DeletePostAsync(id);
        List<CommentDTO> comments = GetCommentsForPost(id);
        foreach (CommentDTO comment in comments)
        {
            await commentRepository.DeleteCommentAsync(comment.CommentId);
        }
        return Results.NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetSinglePost([FromRoute] int id,
        [FromQuery] bool includeAuthor, [FromQuery] bool includeComments)
    {
        IQueryable<Post> queryForPost = postRepository 
            .GetMultiplePosts() 
            .Where(p => p.ID == id) 
            .AsQueryable();
        if (includeAuthor)
        {
            queryForPost = queryForPost.Include(p => p.User);
        }

        if (includeComments)
        {
            queryForPost = queryForPost.Include(p => p.Comments);
        }
        CompletePostDTO? dto = await queryForPost.Select(post => new CompletePostDTO()
        {
            Id = post.ID, 
            Title = post.Title, 
            Content = post.Content, 
            Author = post.User.Name, 
            Comments = includeComments ? post.Comments.Select(c => new CommentDTO
            {
                CommentId = c.Id,
                CommentBody = c.CommentBody, 
                Commenter = c.User.Name
            }).ToList() : new ()
        }) .FirstOrDefaultAsync(); 
        return dto == null ? Results.NotFound() : Results.Ok(dto);
    }

    [HttpGet]
    public IResult GetManyPosts([FromQuery] string? nameContains,
        [FromQuery] int? writtenBy)
    {
        List<Post> posts = postRepository.GetMultiplePosts().ToList();
        if (!string.IsNullOrWhiteSpace(nameContains))
        {
            posts = posts
                .Where(p => p.Title.ToLower().Contains(nameContains.ToLower()))
                .ToList();
        }

        if (writtenBy.HasValue)
        {
            posts = posts.Where(p => p.UserID == writtenBy).ToList();
        }

        return Results.Ok(posts.Select(post => new CompletePostDTO()
        {
            Id = post.ID,
            Content = post.Content,
            Title = post.Title,
            Author = userRepository.GetSingleUserAsync(post.UserID).Result.Name,
            Comments = GetCommentsForPost(post.ID)
        }));
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

    private List<CommentDTO> GetCommentsForPost(int postId)
    {
        List<CommentDTO> commentsForPost = new List<CommentDTO>();
        List<Comment> comments = commentRepository.GetAll().ToList();
        foreach (Comment comment in comments)
        {
            if (comment.PostId == postId)
            {
                User commenter = userRepository
                    .GetSingleUserAsync(comment.UserId).Result;
                CommentDTO com = new()
                {
                    CommentId = comment.Id,
                    CommentBody = comment.CommentBody,
                    Commenter = commenter.Name
                };
                commentsForPost.Add(com);
            }
        }
        return commentsForPost;
    }
}