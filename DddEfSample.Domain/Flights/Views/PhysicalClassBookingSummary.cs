namespace DddEfSample.Domain.Flights.Views
{
    public class PhysicalClassBookingSummary
    {
        public PhysicalClassBookingSummary(PhysicalClassIataCode physicalClass, int numberOfSeatsBooked)
        {
            PhysicalClass = physicalClass;
            NumberOfBookedSeats = numberOfSeatsBooked;
        }

        public PhysicalClassIataCode PhysicalClass { get; }
        public int NumberOfBookedSeats { get; }
    }
}
