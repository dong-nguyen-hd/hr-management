using System.ComponentModel.DataAnnotations;

namespace API.Resources.DTOs.CategoryPerson;

public class CreateCategoryPersonResource
{
    [Required]
    [Display(Name = "Person Id")]
    public int PersonId { get; set; }

    [Required]
    [Display(Name = "Category Id")]
    public int CategoryId { get; set; }

    [Required]
    [MaxLength(50)]
    public List<int> Technologies { get; set; }
}