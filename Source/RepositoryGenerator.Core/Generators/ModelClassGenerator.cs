using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using System.Text;
using RepositoryGenerator.Core.Models;

namespace RepositoryGenerator.Core.Generators
{
    public interface IModelClassGenerator
    {
        string Generate(TableDefinition tableDefinition);
    }

    public class ModelClassGenerator: IModelClassGenerator
    {
        public string Generate(TableDefinition tableDefinition)
        {
            var targetUnit = new CodeCompileUnit();

            var classNamespace = new CodeNamespace("Repositories");
            AddImports(classNamespace);

            var targetClass = new CodeTypeDeclaration("{tableDefinition.Name}")
            {
                IsClass = true,
                TypeAttributes = TypeAttributes.Public | TypeAttributes.Sealed
            };

            classNamespace.Types.Add(targetClass);
            targetUnit.Namespaces.Add(classNamespace);

            AddProperties(tableDefinition, targetClass);

            var provider = CodeDomProvider.CreateProvider("CSharp");
            var options = new CodeGeneratorOptions { BracingStyle = "C" };

            var sb = new StringBuilder();
            using (var sw = new StringWriter(sb))
            {
                provider.GenerateCodeFromCompileUnit(targetUnit, sw, options);
                return sb.ToString().Replace("//;", "");
            }
        }

        private void AddProperties(TableDefinition tableDefinition, CodeTypeDeclaration targetClass)
        {
            foreach (var column in tableDefinition.Columns)
            {
                targetClass.Members.Add(CreateProperty(column.DataType.DotNetType, column.Name));
            }
        }

        private CodeMemberField CreateProperty(Type type, string name)
        {
            var field = new CodeMemberField
            {
                Attributes = MemberAttributes.Public | MemberAttributes.Final,
                Name = name,
                Type = new CodeTypeReference(type)
            };

            field.Name += " { get; set; }//";
            return field;
        }

        private static void AddImports(CodeNamespace classNamespace)
        {
            classNamespace.Imports.Add(new CodeNamespaceImport("System"));
            classNamespace.Imports.Add(new CodeNamespaceImport("System.Data"));
            classNamespace.Imports.Add(new CodeNamespaceImport("System.Linq"));
        }
    }
}