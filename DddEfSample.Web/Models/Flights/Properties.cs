using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DddEfSample.Web.Models.Flights
{
    public class Properties: IValidatableObject
    {
        [Required]
        [MaxLength(100)]
        public string DepartureCity { get; set; }

        [Required]
        [MaxLength(100)]
        public string ArrivalCity { get; set; }

        [Required]
        public DateTimeOffset DepartingAt { get; set; }
        
        public List<PhysicalClassCapacity> Configuration { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var physicalClassesWithDuplicatedCapacity = Configuration?
                .GroupBy(x => x.PhysicalClass)
                .Where(g => g.Skip(1).Any())
                .Select(g => g.Key)
                .ToList();
            if ((physicalClassesWithDuplicatedCapacity?.Count ?? 0) > 0)
            {
                var classesSuffix = physicalClassesWithDuplicatedCapacity.Count > 1 ? "es" : "";
                var physicalClasses = string.Join(", ", physicalClassesWithDuplicatedCapacity);
                yield return new ValidationResult($"Dulicated capacity for physical class{classesSuffix} {physicalClasses}.", new[] { nameof(Configuration) });
            }
        }
    }
}
