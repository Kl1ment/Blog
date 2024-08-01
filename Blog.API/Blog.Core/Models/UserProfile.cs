namespace Blog.API.Models
{
    public class UserProfile
    {
        public int Id { get; }
        public string Nikname { get; }

        private static int _currentId = 1;

        private UserProfile(int id, string nikname)
        {
            Id = id;
            Nikname = nikname;
        }

        static (UserProfile?, string?) Create(string nikname, string email, string password)
        {
            (UserLogin? userLogin, string? error) = UserLogin.Create(email, password);

            if (error != null)
                return (null, error);

            if (string.IsNullOrEmpty(nikname))
            {
                error = "The first or last name cannot be empty.";

                return (null, error);
            }

            int newId = _currentId++;

            return (new UserProfile(newId, nikname), error);
        }
    }
}
