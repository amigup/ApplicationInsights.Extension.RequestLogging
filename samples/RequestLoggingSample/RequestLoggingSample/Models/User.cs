using ApplicationInsights.Extension.RequestLogging.Attributes;
using System;
using System.Collections.Generic;

namespace RequestLoggingSample.Models
{
    public class User
    {
        [PII]
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime AnneviseryDate { get; set; }

        [PII]
        public int LinkId { get; set; }

        public List<Address> Addresses { get; set; }
    }
}
