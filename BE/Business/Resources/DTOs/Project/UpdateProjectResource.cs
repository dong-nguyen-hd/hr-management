﻿using System.ComponentModel.DataAnnotations;
using Business.Extensions.Validation;

namespace Business.Resources.DTOs.Project;

public class UpdateProjectResource
{
    [Required]
    [MaxLength(250)]
    public string Position { get; set; }

    [Required]
    [MaxLength(250)]
    public string Responsibilities { get; set; }

    [Required]
    [StartDate("EndDate")]
    [DataType(DataType.Date)]
    [Display(Name = "Start Date")]
    public DateTime StartDate { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "End Date")]
    public DateTime? EndDate { get; set; }

    [Required]
    [Display(Name = "Group Id")]
    public int GroupId { get; set; }
}