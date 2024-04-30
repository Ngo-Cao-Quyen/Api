
using Api.Dtos.MovieSeries;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Api.Dtos.Region
{
    public class RegionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<MovieSeriesDto>Series { get; set; }
    }

}
