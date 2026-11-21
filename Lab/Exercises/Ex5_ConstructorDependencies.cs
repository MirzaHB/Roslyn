using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RoslynLab.Exercises;

/// <summary>
/// Exercise 5: constructor dependencies. For every class, print the types its
/// constructors ask for. In a dependency-injected app this is the "which
/// services depend on X" graph.
///
/// Output format (full expected output is in EXPECTED.md):
///
///   OrdersController depends on IOrderService
///
/// Classes whose constructors take no parameters print nothing.
///
/// This time, start from symbols instead of syntax. Every class has
/// constructors as symbols even when none is written in the source.
///
/// Useful API:
///   typeSymbol.TypeKind == TypeKind.Class
///   typeSymbol.InstanceConstructors            IMethodSymbol each
///   constructor.Parameters                     IParameterSymbol each
///   parameter.Type.Name
///
/// Getting all type symbols without touching syntax is possible too: start at
/// compilation.Assembly.GlobalNamespace and recurse through
/// GetNamespaceMembers() and GetTypeMembers(). Try it that way if you want the
/// extra practice; going through the trees as in exercise 2 is also fine.
/// </summary>
public static class Ex5_ConstructorDependencies
{
    public static void Run()
    {
        throw new NotImplementedException();
    }
}
