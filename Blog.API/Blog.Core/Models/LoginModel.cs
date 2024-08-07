namespace Blog.API.Models
{
    public class LoginModel
    {
        public int Id { get; }
        public string Email { get; }
        public string PasswordHash { get; }

        private LoginModel(string email, string passwordHash)
        {
            Email = email;
            PasswordHash = passwordHash;
        }

        private LoginModel(int id, string email, string passwordHash)
        {
            Id = id;
            Email = email;
            PasswordHash = passwordHash;
        }

        public static LoginModel Create(string email, string passwordHash)
        {
            return new LoginModel(email, passwordHash);
        }

        public static LoginModel Create(int id, string email, string passwordHash)
        {
            return new LoginModel(id, email, passwordHash);
        }
    }
}
