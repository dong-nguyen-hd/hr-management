using System.ComponentModel.DataAnnotations;

namespace API.Resources.DTOs.Category;

public class FilterCategoryResource
{
    [MaxLength(500)]
    [Display(Name = "Category Name")]
    public string CategoryName { get; set; }

    [MaxLength(500)]
    [Display(Name = "Technology Name")]
    public string TechnologyName { get; set; }
}