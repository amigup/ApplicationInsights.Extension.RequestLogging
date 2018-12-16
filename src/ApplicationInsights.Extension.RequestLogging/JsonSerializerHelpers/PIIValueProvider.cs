using Newtonsoft.Json.Serialization;

namespace ApplicationInsights.Extension.RequestLogging.JsonSerializerHelpers
{
    internal class PIIValueProvider : IValueProvider
    {
        private object defaultValue;

        public PIIValueProvider(string defaultValue)
        {
            this.defaultValue = defaultValue;
        }

        public object GetValue(object target)
        {
            return this.defaultValue;
        }

        public void SetValue(object target, object value)
        {
        }
    }
}
