namespace ActorSystemDemo.Messages
{
    public class PlayMovieMessage
    {
        public string MovieTitle { get; private set; }
        public int UserId { get; private set; }

        public PlayMovieMessage(string movieName, int userId)
        {
            this.MovieTitle = movieName;
            this.UserId = userId;
        }
    }
}