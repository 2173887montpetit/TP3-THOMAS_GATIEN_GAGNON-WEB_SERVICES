namespace ServerFlappybirb.DTOs
{
    public class PublicScoreDTO
    {
        public string Pseudo { get; set; } = null!;
        public double Value { get; set; }
        public double TempsEnSecondes { get; set; }
        public DateTime Date { get; set; }
    }
    public class MyScoreDTO
    {
        public double scoreValue { get; set; }
        public double timeInSeconds { get; set; }
        public DateTime date { get; set; }
        public bool isPublic { get; set; }
    }
}
