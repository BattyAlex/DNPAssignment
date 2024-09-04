using Entities;

namespace RepositoryContracts;

public interface IPostRepository
{
   Task<Post> AddPost(Post post);
   Task<Post> UpdatePost(Post post);
   Task<Post> DeletePost(int  ID);
   Task<Post> GetSinglePost(int ID);
   IQueryable<Post> GetMultiplePosts();
   
}