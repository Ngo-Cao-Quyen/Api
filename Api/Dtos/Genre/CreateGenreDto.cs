using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Genre
{
    public class CreateGenreDto
    {
        [Required]
        public string Name { get; set; }
    }
}
