using ApplicationInsights.Extension.RequestLogging.Attributes;

namespace RequestLoggingSample.Models
{
    public class Address
    {
        [PII]
        public string AddressLine { get; set; }
        
        public string City { get; set; }

        public string Country { get; set; }
    }
}
