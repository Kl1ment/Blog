namespace Blog.API.Models
{
    public class LoginModel
    {
        public int Id { get; }
        public string Email { get; }
        public int HashPassword { get; }

        static private int _currentId = 1;

        private LoginModel(int id, string email, int hashPassword)
        {
            Id = id;
            Email = email;
            HashPassword = hashPassword;
        }

        public static (LoginModel? newUser, string? error) Register(string email, string password)
        {
            string? error = null;

            if (password.Length < 4)
            {
                error = "Short password";

                return (null, error);
            }

            int hashPassword = password.GetHashCode();

            return (new LoginModel(_currentId++, email, hashPassword), error);
        }

        public static LoginModel Login(int id, string email, int hashPassword)
        {
            return new LoginModel(id, email, hashPassword);
        }
    }
}
