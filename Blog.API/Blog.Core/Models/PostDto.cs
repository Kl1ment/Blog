using Blog.Core.Abstractions;

namespace Blog.Core.Models
{
    public class PostDto : IPostModel
    {
        public PostDto(Guid id, int autorId, string title, string textData, DateTime createdDate, int views)
        {
            Id = id;
            AuthorId = autorId;
            Title = title;
            TextData = textData;
            CreatedDate = createdDate;
            Views = views;
        }

        public Guid Id { get; }

        public int AuthorId { get; }

        public string Title { get; } = string.Empty;

        public string TextData { get; } = string.Empty;

        public DateTime CreatedDate { get; }

        public int Views { get; }
    }
}
