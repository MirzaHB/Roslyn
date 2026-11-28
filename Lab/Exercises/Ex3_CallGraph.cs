using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RoslynLab.Exercises;

/// <summary>
/// Exercise 3: the call graph. For every method in SampleApp, print each
/// method it calls. This is the core of the navigator's "trace" feature.
///
/// Output format (full expected output is in EXPECTED.md):
///
///   OrdersController.GetById -> IOrderService.Find
///   OrderService.Place -> OrderService.Validate
///
/// Each side is "ContainingType.Name". List every call, including ones into
/// the framework (Console.WriteLine, List.Add ...).
///
/// Steps:
///   1. For each tree, get its semantic model.
///   2. For each MethodDeclarationSyntax, get the caller's IMethodSymbol with
///      model.GetDeclaredSymbol(method).
///   3. Inside that method, find every InvocationExpressionSyntax.
///   4. Ask the model what is being invoked:
///      model.GetSymbolInfo(invocation).Symbol as IMethodSymbol
///   5. Print caller and callee using symbol.ContainingType.Name and
///      symbol.Name.
///
/// Things to notice in your output:
///   - Calls made through a field of interface type end at the interface
///     (IOrderService.Find), not at OrderService.Find. Exercise 6 fixes that.
///   - "new Order { ... }" is missing. Object creation is a different node:
///     ObjectCreationExpressionSyntax.
///   - FirstOrDefault is an extension method. Which type does it belong to?
///
/// Part two, once part one works: only print calls whose target is declared
/// in SampleApp's own source. Hint: callee.DeclaringSyntaxReferences is empty
/// for symbols that come from a referenced assembly.
/// </summary>
public static class Ex3_CallGraph
{
    public static void Run()
    {
        var compilation = Lab.CreateSampleCompilation();
        foreach (var tree in compilation.SyntaxTrees)
        {
            var root = tree.GetRoot();
            var SemanticModel = compilation.GetSemanticModel(tree);
            foreach (var methodNode in root.DescendantNodes().OfType<MethodDeclarationSyntax>())
            {
                var methodSymbol = SemanticModel.GetDeclaredSymbol(methodNode);
                foreach (var invocation in methodNode.DescendantNodes().OfType<InvocationExpressionSyntax>())
                {
                    var invocationInfo = SemanticModel.GetSymbolInfo(invocation);
                    if (invocationInfo.Symbol != null && invocationInfo.Symbol.DeclaringSyntaxReferences.Any() && methodSymbol != null)
                        Console.WriteLine($"{methodSymbol.ContainingType.Name}.{methodSymbol.Name} -> {invocationInfo.Symbol.ContainingType.Name}.{invocationInfo.Symbol.Name}");
                }
            }
        }
    }
}
