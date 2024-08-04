namespace Blog.DataAccess.Entities
{
    public record class UserLoginEntity
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string passwordHash { get; set; } = string.Empty;
    }
}
