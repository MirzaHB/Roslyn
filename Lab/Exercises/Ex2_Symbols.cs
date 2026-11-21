using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RoslynLab.Exercises;

/// <summary>
/// Exercise 2: from syntax to symbols. For every class and interface, print
/// its fully qualified name, its base class and the interfaces it implements.
///
/// Output format (full expected output is in EXPECTED.md):
///
///   SampleApp.Controllers.OrdersController
///     base: SampleApp.Framework.ControllerBase
///   SampleApp.Data.InMemoryOrderRepository
///     implements: SampleApp.Data.IOrderRepository
///
/// Skip the "base:" line when the base is System.Object or there is none.
///
/// Concept: a symbol (ISymbol) is the compiler's idea of a declared thing: one
/// per type, method, parameter and so on, no matter how many places in the
/// source mention it. A SemanticModel is the bridge from one tree's syntax
/// nodes to symbols. You get one model per tree from the Compilation.
///
/// Useful API:
///   Lab.CreateSampleCompilation()
///   compilation.SyntaxTrees
///   compilation.GetSemanticModel(tree)
///   model.GetDeclaredSymbol(typeDeclaration)   returns INamedTypeSymbol
///   symbol.ToDisplayString()                   fully qualified name
///   symbol.BaseType, symbol.Interfaces
///   symbol.SpecialType == SpecialType.System_Object
///
/// Think about: the syntax for OrdersController only says ": ControllerBase".
/// Where did the namespace in your output come from?
///
/// Also try: print compilation.GetDiagnostics() and then break the sample (a
/// typo in a type name). These are the same errors the compiler would report.
/// </summary>
public static class Ex2_Symbols
{
    public static void Run()
    {
        var compilation = Lab.CreateSampleCompilation();
        foreach (var tree in compilation.SyntaxTrees)
        {
            var sematicModel = compilation.GetSemanticModel(tree);
            var root = tree.GetRoot();
            foreach (var node in root.DescendantNodes())
            {
                if (node is TypeDeclarationSyntax x)
                {
                    var symbol = sematicModel.GetDeclaredSymbol(x);
                    if (symbol is null) continue;
                    var baseType = symbol.BaseType;
                    var interfaces = symbol.Interfaces;
                    Console.WriteLine($"{symbol.ToDisplayString()}");
                    if (symbol.BaseType != null && symbol.BaseType.SpecialType != SpecialType.System_Object)
                        Console.WriteLine($"  base: {baseType}");

                    foreach (var i in interfaces)
                    {
                        Console.WriteLine($"  implements: {i.ToDisplayString()}");
                    }
                }
            }
        }
    }
}
