namespace Blog.API.Models
{
    public class Post
    {
        public Guid Id { get; }
        public string Date { get; }
        public string? Text { get; }
        public int Likes { get; }

        public Post(string? text)
        {
            Date = DateTime.Now.ToLongDateString();
            Text = text;
            Likes = 0;
        }
    }
}
