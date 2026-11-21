using System;

namespace SampleApp.Framework;

// Stand-ins for the ASP.NET Core types of the same name, so the sample has no
// package dependencies. They have the same shape as the real ones, so the code
// you write to find them carries over to a real web project unchanged.

[AttributeUsage(AttributeTargets.Class)]
public sealed class ApiControllerAttribute : Attribute { }

[AttributeUsage(AttributeTargets.Class)]
public sealed class RouteAttribute : Attribute
{
    public RouteAttribute(string template) => Template = template;
    public string Template { get; }
}

[AttributeUsage(AttributeTargets.Method)]
public sealed class HttpGetAttribute : Attribute
{
    public HttpGetAttribute(string template = "") => Template = template;
    public string Template { get; }
}

[AttributeUsage(AttributeTargets.Method)]
public sealed class HttpPostAttribute : Attribute
{
    public HttpPostAttribute(string template = "") => Template = template;
    public string Template { get; }
}

public abstract class ControllerBase { }
