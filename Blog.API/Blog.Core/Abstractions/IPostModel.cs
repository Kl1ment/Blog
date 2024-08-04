namespace Blog.Core.Abstractions
{
    public interface IPostModel
    {
        int AuthorId { get; }
        DateTime CreatedDate { get; }
        Guid Id { get; }
        string TextData { get; }
        string Title { get; }
        int Views { get; }
    }
}