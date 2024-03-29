﻿using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FS.TimeTracking.ReportServer.Api.REST.Filters;

internal class AddCSharpActionFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var action = context.MethodInfo.Name;
        operation.Extensions.Add("x-csharp-action", new OpenApiString(action));
    }
}