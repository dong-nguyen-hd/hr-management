using System.ComponentModel.DataAnnotations;

namespace API.Resources.DTOs.CategoryPerson;

public class UpdateCategoryPersonResource
{
    [Required]
    [Display(Name = "Category Id")]
    public int CategoryId { get; set; }

    [Required]
    [MaxLength(50)]
    public List<int> Technologies { get; set; }
}