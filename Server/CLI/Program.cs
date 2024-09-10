// See https://aka.ms/new-console-template for more information

using CLI.UI.ManagePosts;
using InMemoryRepositories;
using RepositoryContracts;

Console.WriteLine("Starting CLI app...");

IUserRepository userRepository = new UserInMemoryRepository();
ICommentRepository commentRepository = new CommentInMemoryRepository();
IPostRepository postRepository = new PostInMemoryRepository();

ViewHandler viewHandler = new ViewHandler(userRepository, commentRepository, postRepository);
await viewHandler.StartAsync();
