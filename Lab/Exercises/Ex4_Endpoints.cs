using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RoslynLab.Exercises;

/// <summary>
/// Exercise 4: endpoint discovery. Find every method marked [HttpGet] or
/// [HttpPost] and print the HTTP verb, the full route and the handler.
///
/// Output format (full expected output is in EXPECTED.md):
///
///   GET /orders/{id} -> OrdersController.GetById
///
/// The route is the class's [Route] template joined with the method's own
/// template. A method with an empty template maps to the class route alone.
///
/// Concept: attributes are data attached to symbols. Reading them from the
/// symbol (not from syntax) gives you the resolved attribute class and the
/// constant values of its arguments.
///
/// Useful API:
///   methodSymbol.GetAttributes()               also on type symbols
///   attribute.AttributeClass?.Name             "HttpGetAttribute"
///   attribute.ConstructorArguments[0].Value    the template, as object?
///   methodSymbol.ContainingType                to reach the class's [Route]
///
/// Think about: [HttpPost] on Create passes no argument, yet
/// ConstructorArguments still has one entry. Why?
/// </summary>
public static class Ex4_Endpoints
{
    public static void Run()
    {
        throw new NotImplementedException();
    }
}
