namespace DddEfSample.Domain.Flights
{
    public class PhysicalClassCapacity
    {
        public PhysicalClassCapacity(PhysicalClassIataCode physicalClass, int capacity)
        {
            PhysicalClass = physicalClass;
            Capacity = capacity;
        }

        public PhysicalClassIataCode PhysicalClass { get; }
        public int Capacity { get; }
    }
}
