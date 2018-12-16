using ApplicationInsights.Extension.RequestLogging.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationInsights.Extension.RequestLogging.JsonSerializerHelpers
{
    internal class PIILogContractResolver : DefaultContractResolver
    {
        private static readonly ConcurrentDictionary<Type, TypeMetaData> cacheTypes = new ConcurrentDictionary<Type, TypeMetaData>();

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var properties = new List<JsonProperty>();

            if (!cacheTypes.ContainsKey(type))
            {
                var hasPIIAttribute = type.GetCustomAttributes(true).Any(t => t.GetType() == typeof(PIIAttribute));
                var definedPIIProperties = type.GetProperties().Where(p => p.GetCustomAttributes(true).Any(t => t.GetType() == typeof(PIIAttribute))).Select(s => s.Name);
                cacheTypes.TryAdd(type, new TypeMetaData(hasPIIAttribute, definedPIIProperties));
            }

            if (cacheTypes.TryGetValue(type, out TypeMetaData typeMetaData))
            {
                if (!typeMetaData.HasPIIAttribute)
                {
                    IList<JsonProperty> retval = base.CreateProperties(type, memberSerialization);
                    var excludedProperties = typeMetaData.Properties;
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
            }

            return properties;
        }

        private class TypeMetaData
        {
            public TypeMetaData(bool hasPIIAttribute, IEnumerable<string> properties)
            {
                HasPIIAttribute = hasPIIAttribute;
                Properties = properties;
            }

            public bool HasPIIAttribute { get; }
            public IEnumerable<string> Properties { get; }
        }
    }
}
