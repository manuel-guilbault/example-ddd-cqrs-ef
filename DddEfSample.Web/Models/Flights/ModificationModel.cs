﻿using System.ComponentModel.DataAnnotations;

namespace DddEfSample.Web.Models.Flights
{
    public class ModificationModel
    {
        [Required]
        public string ETag { get; set; }

        [Required]
        public ConfigurationModel Configuration { get; set; }
    }
}
