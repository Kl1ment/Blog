namespace Blog.API.Models
{
    public class UserLogin
    {
        public string? Email { get; }
        public int HashPassword { get; }

        private UserLogin(string? email, int hashPassword)
        {
            Email = email;
            HashPassword = hashPassword;
        }

        public static (UserLogin?, string?) Create(string email, string password)
        {
            string? error = null;

            if (password.Length < 4)
            {
                error = "Short password";

                return (null, error);
            }

            int hashPassword = password.GetHashCode();

            return (new UserLogin(email, hashPassword), error);
        }

        public static UserLogin Create(string email, int hashPassword)
        {
            return new UserLogin(email, hashPassword);
        }
    }
}
