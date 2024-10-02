﻿using DataTransferObjects;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;


namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController
{
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;

    public PostsController(IPostRepository postRepository, ICommentRepository commentRepository)
    {
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
    }

    [HttpPost]
    public async Task<IResult> CreatePost([FromBody] CreatePostDTO post)
    {

        Post newPost = new()
        {
            Content = post.Text
        };
        Post created = await postRepository.AddPostAsync(newPost);
        return Results.Created($"/Posts/{created.ID}", created);
    }

    [HttpPut]
    public async Task<IResult> UpdatePost([FromRoute] int id, [FromBody] ReplacePostDTO request)
    {
        UpdatePostDTO post = new()
        {
            id = id
        };
        Post updated = new()
        {
            ID = post.id,
        };
        await postRepository.UpdatePostAsync(updated, request);
        return Results.NoContent();
    }

    [HttpDelete]
    public async Task<IResult> DeletePost([FromRoute] int id)
    {
        await postRepository.DeletePostAsync(id);
        return Results.NoContent();
    }

    [HttpGet]
    public async Task<IResult> GetSinglePost([FromRoute] int id)
    {
        try
        {
            Post result = await postRepository.GetSinglePostAsync(id);
            return Results.Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Results.NotFound(e.Message);
        }
    }

    [HttpGet]
        public IResult GetManyPosts([FromQuery] int id, [FromRoute] string? nameContains)
        {
            List<Post> posts = postRepository.GetMultiplePosts().ToList();
            if (id != null)
            {
                posts = posts.Where(p => p.ID == id).ToList();
            }

            if (!string.IsNullOrWhiteSpace(nameContains))
            {
                posts = posts.Where(p => p.Title.ToLower().Contains(nameContains.ToLower())).ToList();
            }

            return Results.Ok(posts);
        }
}
