// See https://aka.ms/new-console-template for more information

using CLI.UI.ManagePosts;
using FileRepositories;
using InMemoryRepositories;
using RepositoryContracts;

Console.WriteLine("Starting CLI app...");

IUserRepository userRepository = new UserFileRepository();
ICommentRepository commentRepository = new CommentInMemoryRepository();
IPostRepository postRepository = new PostFileRepository();

ViewHandler viewHandler = new ViewHandler(userRepository, commentRepository, postRepository);
await viewHandler.StartAsync();
