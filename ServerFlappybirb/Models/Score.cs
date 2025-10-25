namespace ServerFlappybirb.Models
{
    public class Score
    {

       public int id { get; set; }
        public string? pseudo { get; set; }

        public string? date { get; set; }

        public int timeInSeconds { get; set; }

        public int scoreValue { get; set; }

        public bool isPublic { get; set; }
    }
}
