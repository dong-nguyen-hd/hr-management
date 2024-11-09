﻿using System.ComponentModel.DataAnnotations;
using Business.Resources.DTOs.Technology;

namespace Business.Resources.DTOs.CategoryPerson;

public class CategoryPersonResource
{
    [Required]
    public int Id { get; set; }

    [Required]
    [Display(Name = "Order Index")]
    public int OrderIndex { get; set; }

    [Required]
    [Display(Name = "Category Id")]
    public int CategoryId { get; set; }

    [Required]
    [Display(Name = "Category Name")]
    public string CategoryName { get; set; }

    [Required]
    public List<TechnologyResource> Technologies { get; set; } = new ();
}