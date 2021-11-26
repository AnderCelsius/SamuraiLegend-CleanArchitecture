namespace SamuraiLegend.Domain.Entities
{
    public class Quote
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Samurai Samurai { get; set; }
        public string SamuraiId { get; set; }
    }
}