---
platforms: ASP.NET Core 2.2 Web API, Application Insights
author: amigup
---

# ApplicationInsights.Extension.RequestLogging
Implements to log Request Body in application insights after removing the sensitive information.

### Scenario

You want to log the Request Body in application Insights after removing the sensitive information so that in Appliation Insights the request body payload can be viewed.

### Details

This package is built on 
- ASP.Net Core 2.2 Web API.
- Microsoft.ApplicationInsights 2.8.1
- Newtonsoft.Json 12.0.1

### Sample Application
The repository contains a sample application to demostrate the usage of `ApplicationInsights.Extension.RequestLogging`.
![Application Insights](./ApplicationInsightsRequetJsonBody.jpg)
