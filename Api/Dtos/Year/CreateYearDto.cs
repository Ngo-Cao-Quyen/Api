using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Year
{
    public class CreateYearDto
    {
        [Required]
        public int Name { get; set; }
    }
}
