namespace Blog.Core.Models
{
    public class UserModel
    {
        public int Id { get; }
        public string UserName { get; }

        private UserModel(int id, string userName)
        {
            Id = id;
            UserName = userName;
        }

        public static UserModel Create(int id, string userName)
        {
            return new UserModel(id, userName);
        }
    }
}
