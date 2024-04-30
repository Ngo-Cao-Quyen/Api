namespace Api.Models
{
    public class Year
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public List<MovieSeries> Series { get; set; } = new List<MovieSeries>();
    }
}
