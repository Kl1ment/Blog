using Blog.Core.Abstractions;

namespace Blog.Core.Models
{
    public class PostResponse : IPostModel
    {
        public PostResponse(Guid id, int authorId, string title, string textData, DateTime createdDate, int views)
        {
            Id = id; 
            AuthorId = authorId; 
            Title = title; 
            TextData = textData;
            CreatedDate = createdDate;
            Views = views;
        }

        public Guid Id { get; }

        public int AuthorId { get; }

        public DateTime CreatedDate { get; }

        public string TextData { get; }

        public string Title { get; }

        public int Views { get; }
    }
}
