using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DddEfSample.Web.Models.Flights
{
    public class ConfigurationModel: List<PhysicalClassCapacityModel>, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var duplicatedPhysicalClasses = this
                .GroupBy(x => x.PhysicalClass)
                .Where(g => g.Skip(1).Any())
                .Select(g => g.Key)
                .ToList();
            if (duplicatedPhysicalClasses.Count > 0)
            {
                var classesSuffix = duplicatedPhysicalClasses.Count > 1 ? "es" : "";
                var physicalClasses = string.Join(", ", duplicatedPhysicalClasses);
                yield return new ValidationResult($"Dulicated capacity for physical class{classesSuffix} {physicalClasses}.");
            }
        }
    }
}
