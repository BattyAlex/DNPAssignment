// See https://aka.ms/new-console-template for more information

using CLI.UI.ManagePosts;
using FileRepositories;
using RepositoryContracts;

Console.WriteLine("Starting CLI app...");

IUserRepository userRepository = new UserFileRepository();
ICommentRepository commentRepository = new CommentFileRepository();
IPostRepository postRepository = new PostFileRepository();

ViewHandler viewHandler = new ViewHandler(userRepository, commentRepository, postRepository);
await viewHandler.StartAsync();
