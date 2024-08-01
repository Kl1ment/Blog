namespace Blog.DataAccess.Entities
{
    public record class UserLoginEntity
    {
        public string? Email { get; set; }
        public int HashPassword { get; set; }
    }
}
