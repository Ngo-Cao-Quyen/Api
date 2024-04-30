using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Genre
{
    public class UpdateGenreDto
    {
        [Required]
        public string Name { get; set; }
    }
}
