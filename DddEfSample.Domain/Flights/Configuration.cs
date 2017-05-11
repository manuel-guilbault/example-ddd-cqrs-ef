using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DddEfSample.Domain.Flights
{
    public class Configuration : IEnumerable<PhysicalClassCapacity>
    {
        private readonly Dictionary<PhysicalClassIataCode, PhysicalClassCapacity> _physicalClasses;

        public Configuration(IEnumerable<PhysicalClassCapacity> physicalClasses)
        {
            _physicalClasses = physicalClasses.ToDictionary(x => x.PhysicalClass);
        }

        public int TotalCapacity => _physicalClasses.Values.Sum(x => x.Capacity);

        public PhysicalClassCapacity this[PhysicalClassIataCode physicalClass]
        {
            get { return _physicalClasses[physicalClass]; }
        }

        public bool Contains(PhysicalClassIataCode physicalClass)
        {
            return _physicalClasses.ContainsKey(physicalClass);
        }

        public bool TryGet(PhysicalClassIataCode physicalClass, out PhysicalClassCapacity result)
        {
            return _physicalClasses.TryGetValue(physicalClass, out result);
        }

        public IEnumerator<PhysicalClassCapacity> GetEnumerator()
        {
            return _physicalClasses.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
