using ApplicationInsights.Extension.RequestLogging.Attributes;
using ApplicationInsights.Extension.RequestLogging.JsonSerializerHelpers;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationInsights.Extension.RequestLogging.Filters
{
    public class RequestLogActionFilterAttribute : ActionFilterAttribute
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public RequestLogActionFilterAttribute(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.Request.Method == HttpMethods.Post || context.HttpContext.Request.Method == HttpMethods.Put)
            {
                // Check parameter those are marked for not to log.
                var methodInfo = ((ControllerActionDescriptor)context.ActionDescriptor).MethodInfo;
                var noLogParameters = methodInfo.GetParameters().Where(p => p.GetCustomAttributes(true).Any(t => t.GetType() == typeof(PIIAttribute))).Select(p => p.Name);

                StringBuilder logBuilder = new StringBuilder();

                foreach (var argument in context.ActionArguments.Where(a => !noLogParameters.Contains(a.Key)))
                {
                    var serializedModel = JsonConvert.SerializeObject(argument.Value, new JsonSerializerSettings() { ContractResolver = new PIILogContractResolver() });
                    logBuilder.AppendLine($"key: {argument.Key}; value : {serializedModel}");
                }

                var telemetry = this.httpContextAccessor.HttpContext.Items[Constants.ApplicationInsightsExtensionRequestLoggingKey] as RequestTelemetry;
                if (telemetry != null)
                {
                    telemetry.Context.GlobalProperties.Add(Constants.RequestBody, logBuilder.ToString());
                }

            }

            await next();
        }
    }
}
