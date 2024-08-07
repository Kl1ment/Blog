namespace Blog.DataAccess.Entities
{
    public record UserEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;

        public LoginEntity? UserLogin { get; set; }
        public List<PostEntity> Posts { get; set; } = [];
        public List<UserEntity> Subscriptions { get; set; } = [];
        public List<UserEntity> Folowers { get; set; } = [];
    }
}
