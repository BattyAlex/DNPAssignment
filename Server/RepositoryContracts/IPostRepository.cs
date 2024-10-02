using DataTransferObjects;
using Entities;

namespace RepositoryContracts;

public interface IPostRepository
{
   Task<Post> AddPostAsync(Post post);
   Task UpdatePostAsync(Post post, ReplacePostDTO request);
   Task DeletePostAsync(int  id);
   Task<Post> GetSinglePostAsync(int id);
   IQueryable<Post> GetMultiplePosts();
  
   
   
}