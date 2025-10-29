namespace ServerFlappybirb.Models
{
    public class Score
    {

       public int id { get; set; }
        public string pseudo { get; set; }

        public DateTime date { get; set; }

        public double timeInSeconds { get; set; }

        public int scoreValue { get; set; }

        public bool isPublic { get; set; }
    }
}
