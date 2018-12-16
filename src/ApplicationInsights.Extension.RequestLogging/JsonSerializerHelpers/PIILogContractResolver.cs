using ApplicationInsights.Extension.RequestLogging.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationInsights.Extension.RequestLogging.JsonSerializerHelpers
{
    internal class PIILogContractResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var properties = new List<JsonProperty>();

            if (!type.GetCustomAttributes(true).Any(t => t.GetType() == typeof(PIIAttribute)))
            {
                IList<JsonProperty> retval = base.CreateProperties(type, memberSerialization);
                var excludedProperties = type.GetProperties().Where(p => p.GetCustomAttributes(true).Any(t => t.GetType() == typeof(PIIAttribute))).Select(s => s.Name);
                foreach (var property in retval)
                {
                    if (excludedProperties.Contains(property.PropertyName))
                    {
                        property.PropertyType = typeof(string);
                        property.ValueProvider = new PIIValueProvider("PII Data");
                    }

                    properties.Add(property);
                }
            }

            return properties;
        }
    }
}
