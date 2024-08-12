namespace Blog.Core.Abstractions
{
    public interface IPostModel
    {
        Guid Id { get; }
        string Title { get; }
        string TextData { get; }
        int AuthorId { get; }
        DateTime CreatedDate { get; }
        int Views { get; }
    }
}