using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Year
{
    public class UpdateYearDto
    {
        [Required]
        public int Name { get; set; }
    }
}
