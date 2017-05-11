namespace DddEfSample.Domain.Flights
{
    public partial class Flight
    {
        private Configuration _configuration;
        public Configuration Configuration => _configuration;

        public Result<ConfigurationError> UpdateConfiguration(Configuration configuration)
        {
            if (configuration.IsOverBooked(Bookings))
            {
                return Result.Failure(ConfigurationError.WouldCauseOverbooking);
            }

            _configuration = configuration;
            return Result.Success<ConfigurationError>();
        }

        public enum ConfigurationError
        {
            WouldCauseOverbooking
        }
    }
}
