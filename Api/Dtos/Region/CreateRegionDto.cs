using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Region
{
    public class CreateRegionDto
    {
        [Required]
        public string Name { get; set; }
    }
}
