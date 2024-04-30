using System.Text.Json.Serialization;

namespace Api.Models
{
    public class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<MovieSeries> Series { get; set; } = new List<MovieSeries>();
    }
}
