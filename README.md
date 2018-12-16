---
platforms: ASP.NET Core 2.2 Web API, Application Insights
author: amigup
---

# ApplicationInsights.Extension.RequestLogging
Implements to log Request Body in application insights after removing the sensitive information.

[![Build Status](https://dev.azure.com/amigup/amigup/_apis/build/status/amigup.ApplicationInsights.Extension.RequestLogging)](https://dev.azure.com/amigup/amigup/_apis/build/status/amigup.ApplicationInsights.Extension.RequestLogging)

### Scenario

You want to log the Request Body in application Insights after removing the sensitive information so that in Appliation Insights the request body payload can be viewed.

### Details

This package is built on 
- ASP.Net Core 2.2 Web API.
- Microsoft.ApplicationInsights 2.8.1
- Newtonsoft.Json 12.0.1

### Setup

1. Add `RequestLogActionFilterAttribute` filter in the MVC pipeline.
```
services.AddMvc(options =>
 {
       options.Filters.Add<RequestLogActionFilterAttribute>();
 });
```

2. Register the `PIITelemetryInitializer`

```
services.AddSingleton<ITelemetryInitializer, PIITelemetryInitializer>();
```


### Sample Application
The repository contains a sample application to demostrate the usage of `ApplicationInsights.Extension.RequestLogging`.
![Application Insights](./ApplicationInsightsRequetJsonBody.jpg)
