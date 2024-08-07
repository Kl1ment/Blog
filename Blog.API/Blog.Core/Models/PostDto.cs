using Blog.Core.Abstractions;

namespace Blog.Core.Models
{
    public class PostDto : IPostModel
    {
        public PostDto(Guid id, int autorId, DateTime createdDate, string textData, string title, int views)
        {
            Id = id;
            AuthorId = autorId;
            CreatedDate = createdDate;
            TextData = textData;
            Title = title;
            Views = views;
        }

        public Guid Id { get; }

        public int AuthorId { get; }

        public DateTime CreatedDate { get; }

        public string TextData { get; } = string.Empty;

        public string Title { get; } = string.Empty;

        public int Views { get; }
    }
}
