﻿using System.ComponentModel.DataAnnotations;

namespace Business.Resources.DTOs.Department;

public class DepartmentResource
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(250)]
    public string Name { get; set; }
}