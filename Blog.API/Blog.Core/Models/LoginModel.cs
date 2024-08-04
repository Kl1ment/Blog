namespace Blog.API.Models
{
    public class LoginModel
    {
        public int Id { get; }
        public string Email { get; }
        public string PasswordHash { get; }

        static private int _currentId = 1;

        private LoginModel(int id, string email, string passwordHash)
        {
            Id = id;
            Email = email;
            PasswordHash = passwordHash;
        }

        public static LoginModel Register(string email, string passwordHash)
        {
            return new LoginModel(_currentId++, email, passwordHash);
        }

        public static LoginModel Login(int id, string email, string passwordHash)
        {
            return new LoginModel(id, email, passwordHash);
        }
    }
}
