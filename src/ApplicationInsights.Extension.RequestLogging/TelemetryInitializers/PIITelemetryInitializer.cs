using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace ApplicationInsights.Extension.RequestLogging.TelemetryInitializers
{
    public class PIITelemetryInitializer : ITelemetryInitializer
    {
        IHttpContextAccessor httpContextAccessor;

        public PIITelemetryInitializer(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public void Initialize(ITelemetry telemetry)
        {
            if (this.httpContextAccessor.HttpContext != null)
            {
                if (telemetry is RequestTelemetry)
                {
                    this.httpContextAccessor.HttpContext.Items.TryAdd(Constants.ApplicationInsightsExtensionRequestLoggingKey, telemetry);
                }
            }
        }
    }
}
