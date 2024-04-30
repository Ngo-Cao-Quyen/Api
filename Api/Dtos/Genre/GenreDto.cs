
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Api.Dtos.Genre
{
    public class GenreDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
