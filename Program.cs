using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeGenerator
{
  class Program
  {
    static void Main(string[] args)
    {
      var comp = SyntaxFactory.CompilationUnit();
      var us = SyntaxFactory.UsingDirective(
          SyntaxFactory.ParseName("RepoDb.Attributes")
        );
      comp = comp.WithUsings(SyntaxFactory.SingletonList<UsingDirectiveSyntax>(us));
      var cl = SyntaxFactory.ClassDeclaration("tabla1")
            .AddModifiers(new SyntaxToken[] {
              SyntaxFactory.Token(SyntaxKind.PublicKeyword),
              SyntaxFactory.Token(SyntaxKind.PartialKeyword)
            })
            .WithAttributeLists(
              SyntaxFactory.SingletonList(
                SyntaxFactory.AttributeList(
                  SyntaxFactory.SingletonSeparatedList<AttributeSyntax>(
                    SyntaxFactory.Attribute(
                      SyntaxFactory.ParseName("Map"),
                      SyntaxFactory.ParseAttributeArgumentList("(\"table1\")")
                    )
                  )
                )
              )
            );
      var prop = SyntaxFactory.PropertyDeclaration(SyntaxFactory.ParseTypeName("int"), "prop1");
      prop = prop.AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));
      prop = prop.AddAttributeLists(SyntaxFactory.AttributeList(
          SyntaxFactory.SingletonSeparatedList(
            SyntaxFactory.Attribute(
              SyntaxFactory.ParseName("Map"),
              SyntaxFactory.ParseAttributeArgumentList("(\"prop1\")")
            )
          )
        ));
      prop = prop.AddAccessorListAccessors(
        SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
        SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)));
      cl = cl.WithMembers(SyntaxFactory.SingletonList<MemberDeclarationSyntax>(prop));
      var nm = SyntaxFactory.NamespaceDeclaration(
        SyntaxFactory.IdentifierName("DataAccess.Entities")).WithMembers(
          SyntaxFactory.SingletonList<MemberDeclarationSyntax>(cl)
        );
      comp = comp.WithMembers(SyntaxFactory.SingletonList<MemberDeclarationSyntax>(nm));
      Console.WriteLine(comp.NormalizeWhitespace().ToString());
    }
  }
}
