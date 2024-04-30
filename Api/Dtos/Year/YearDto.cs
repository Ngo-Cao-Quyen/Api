
using Api.Dtos.MovieSeries;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Api.Dtos.Year
{
    public class YearDto
    {
        public int Id { get; set; }
        [Required]
        public int Name { get; set; }
        public List<MovieSeriesDto> Series { get; set; }
    }
}
