﻿using HR_Management.Extensions;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace HR_Management.Resources.Education
{
    public class EducationResource
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string CollegeName { get; set; }

        [Required]
        [MaxLength(250)]
        public string Major { get; set; }

        [Required]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime StartDate { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? EndDate { get; set; }

        [Required]
        public int OrderIndex { get; set; }
    }
}
