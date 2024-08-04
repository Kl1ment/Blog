using Blog.Core.Abstractions;
using CSharpFunctionalExtensions;

namespace Blog.Core.Models
{
    public class PostModel : IPostModel
    {
        public const int MAX_TITLE_LENGTH = 250;

        public Guid Id { get; }
        public int AuthorId { get; }
        public string Title { get; } = string.Empty;
        public string TextData { get; } = string.Empty;
        public DateTime CreatedDate { get; }
        public int Views { get; private set; } = 0;

        private PostModel(Guid id, int authorId, string title, string textData, DateTime dateTime)
        {
            Id = id;
            AuthorId = authorId;
            Title = title;
            TextData = textData;
            CreatedDate = dateTime;
        }

        public static Result<PostModel> Create(Guid id, int authorId, string title, string textData)
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(textData))
            {
                return Result.Failure<PostModel>("Название и содержимое поста должны иметь контент");
            }

            var post = new PostModel(id, authorId, title, textData, DateTime.Now);

            return Result.Success<PostModel>(post);
        }
    }
}
