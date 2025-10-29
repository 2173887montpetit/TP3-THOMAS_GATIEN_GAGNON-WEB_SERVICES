namespace ServerFlappybirb.DTOs
{
    public class PublicScoreDTO
    {
        public string Pseudo { get; set; } = null!;
        public double Value { get; set; }
        public double TempsEnSecondes { get; set; }
        public DateTime Date { get; set; }
    }
}
