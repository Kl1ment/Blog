namespace Blog.DataAccess.Entities
{
    public record PostEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string TextData { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public int Views { get; set; } = 0;

        public int AuthorId { get; set; }
        public UserEntity? Author { get; set; }
    }
}
