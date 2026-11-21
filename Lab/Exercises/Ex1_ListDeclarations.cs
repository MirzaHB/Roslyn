using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RoslynLab.Exercises;

/// <summary>
/// Exercise 1: syntax only. For every file in SampleApp, print each class and
/// interface it declares, with the methods underneath.
///
/// Output format (full expected output is in EXPECTED.md):
///
///   Controllers/OrdersController.cs
///     class OrdersController
///       GetById(1)
///       Create(2)
///       Health(0)
///
/// The number is the parameter count.
///
/// Useful API:
///   Lab.ParseSampleTrees(), Lab.RelativePath(tree)
///   tree.GetRoot().DescendantNodes().OfType&lt;T&gt;()
///   TypeDeclarationSyntax     base class of ClassDeclarationSyntax and
///                             InterfaceDeclarationSyntax; has .Keyword,
///                             .Identifier and .Members
///   MethodDeclarationSyntax   .Identifier, .ParameterList.Parameters
///
/// Think about: constructors do not show up as MethodDeclarationSyntax. What
/// node type are they? (Ex0's Dump method can tell you.)
/// </summary>
public static class Ex1_ListDeclarations
{
    public static void Run()
    {
        foreach (var tree in Lab.ParseSampleTrees())
        {
            var root = tree.GetRoot();
            Console.WriteLine(Lab.RelativePath(tree));
            foreach (var node in root.DescendantNodes())
            {
                //first if is for both class and interface cause both are of TypeDeclarationSyntax
                if (node is TypeDeclarationSyntax x)
                    Console.WriteLine($"  {x.Keyword.Text} {x.Identifier.Text}");

                else if (node is MethodDeclarationSyntax y)
                    Console.WriteLine($"    {y.Identifier.Text}({y.ParameterList.Parameters.Count})");
            }
        }
    }
}
