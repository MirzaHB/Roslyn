using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RoslynLab.Exercises;

/// <summary>
/// Exercise 6: seeing through interfaces. In exercise 3 the trace from
/// OrdersController stopped at IOrderService. Here you make it continue into
/// the class that implements the interface.
///
/// Part A: print which class implements each interface.
///
///   IOrderService is implemented by OrderService
///
/// Part B: print the call graph from exercise 3 (source-declared targets
/// only), but replace interface methods with their implementation.
///
///   OrdersController.GetById -> OrderService.Find (via IOrderService.Find)
///   OrderService.Place -> OrderService.Validate
///
/// Full expected output is in EXPECTED.md.
///
/// Useful API:
///   typeSymbol.AllInterfaces                   includes inherited interfaces
///   SymbolEqualityComparer.Default             compare symbols with this,
///                                              never with == or Equals
///   callee.ContainingType.TypeKind == TypeKind.Interface
///   implementingType.FindImplementationForInterfaceMember(interfaceMethod)
///
/// Think about: this sample has exactly one implementation per interface.
/// What should a trace show when there are two? What would tell you which one
/// the running app really uses? (That is what the DI registrations are for in
/// the real project.)
/// </summary>
public static class Ex6_InterfaceResolution
{
    public static void Run()
    {
        throw new NotImplementedException();
    }
}
