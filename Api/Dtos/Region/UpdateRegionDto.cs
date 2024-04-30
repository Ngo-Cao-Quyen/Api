using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Region
{
    public class UpdateRegionDto
    {
        [Required]
        public string Name { get; set; }
    }
}
