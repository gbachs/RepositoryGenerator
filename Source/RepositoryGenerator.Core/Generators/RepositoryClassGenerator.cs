using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using System.Text;
using RepositoryGenerator.Core.Generators.Interfaces;
using RepositoryGenerator.Core.Models;

namespace RepositoryGenerator.Core.Generators
{
    public class RepositoryClassGenerator : IRepositoryClassGenerator
    {
        private readonly ISqlCommandGenerator _sqlCommandGenerator;

        public RepositoryClassGenerator( ISqlCommandGenerator sqlCommandGenerator)
        {
            _sqlCommandGenerator = sqlCommandGenerator;
        }

        public string Create(TableDefinition tableDefinition)
        {
            var targetUnit = new CodeCompileUnit();

            var classNamespace = new CodeNamespace("Repositories");
            AddImports(classNamespace);

            var targetClass = new CodeTypeDeclaration($"{tableDefinition.Name}Repository")
            {
                IsClass = true,
                TypeAttributes = TypeAttributes.Public | TypeAttributes.Sealed
            };

            classNamespace.Types.Add(targetClass);
            targetUnit.Namespaces.Add(classNamespace);

            AddFields(targetClass);
            AddInsertMethod(tableDefinition, targetClass);
            AddLoadMethod(tableDefinition, targetClass);
            AddUpdateMethod(tableDefinition, targetClass);
            AddDeleteMethod(tableDefinition, targetClass);
            AddConstructor(targetClass);

            var provider = CodeDomProvider.CreateProvider("CSharp");
            var options = new CodeGeneratorOptions { BracingStyle = "C" };

            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            provider.GenerateCodeFromCompileUnit(targetUnit, sw, options);
            return sb.ToString();
        }

        private static void AddImports(CodeNamespace classNamespace)
        {
            classNamespace.Imports.Add(new CodeNamespaceImport("System"));
            classNamespace.Imports.Add(new CodeNamespaceImport("System.Data"));
            classNamespace.Imports.Add(new CodeNamespaceImport("System.Linq"));
        }

        public void AddConstructor(CodeTypeDeclaration targetClass)
        {
            var constructor = new CodeConstructor { Attributes = MemberAttributes.Public | MemberAttributes.Final };

            constructor.Parameters.Add(new CodeParameterDeclarationExpression("SqlDatabase", "sqlDatabase"));

            var sqlDatabaseReference = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "_db");
            constructor.Statements.Add(new CodeAssignStatement(sqlDatabaseReference, new CodeArgumentReferenceExpression("sqlDatabase")));

            targetClass.Members.Add(constructor);
        }

        private void AddDeleteMethod(TableDefinition tableDefinition, CodeTypeDeclaration targetClass)
        {
            var insertMethod = new CodeMemberMethod
            {
                Attributes = MemberAttributes.Public | MemberAttributes.Final,
                Name = "Delete"
            };

            insertMethod.Parameters.Add(new CodeParameterDeclarationExpression(tableDefinition.Name, tableDefinition.Name.ToLower()));
            insertMethod.Statements.Add(new CodeSnippetExpression(_sqlCommandGenerator.CreateForDelete(tableDefinition)));

            targetClass.Members.Add(insertMethod);
        }

        private void AddInsertMethod(TableDefinition tableDefinition, CodeTypeDeclaration targetClass)
        {
            var insertMethod = new CodeMemberMethod
            {
                Attributes = MemberAttributes.Public | MemberAttributes.Final,
                Name = "Insert"
            };

            insertMethod.Parameters.Add(new CodeParameterDeclarationExpression(tableDefinition.Name, tableDefinition.Name.ToLower()));
            insertMethod.Statements.Add(new CodeSnippetExpression(_sqlCommandGenerator.CreateForInsert(tableDefinition)));

            targetClass.Members.Add(insertMethod);
        }

        private void AddLoadMethod(TableDefinition tableDefinition, CodeTypeDeclaration targetClass)
        {
            var loadMethod = new CodeMemberMethod
            {
                Attributes = MemberAttributes.Public | MemberAttributes.Final,
                ReturnType = new CodeTypeReference(tableDefinition.Name),
                Name = "Load"
            };

            foreach (var primaryKey in tableDefinition.PrimaryKeys)
            {
                loadMethod.Parameters.Add(new CodeParameterDeclarationExpression(primaryKey.DataType.DotNetType, primaryKey.Name.ToLower()));
            }

            loadMethod.Statements.Add(new CodeSnippetExpression(_sqlCommandGenerator.CreateForSelect(tableDefinition)));

            targetClass.Members.Add(loadMethod);
        }

        private void AddUpdateMethod(TableDefinition tableDefinition, CodeTypeDeclaration targetClass)
        {
            var updateMethod = new CodeMemberMethod
            {
                Attributes = MemberAttributes.Public | MemberAttributes.Final,
                Name = "Update"
            };

            updateMethod.Parameters.Add(new CodeParameterDeclarationExpression(tableDefinition.Name, tableDefinition.Name.ToLower()));
            updateMethod.Statements.Add(new CodeSnippetExpression(_sqlCommandGenerator.CreateForUpdate(tableDefinition)));

            targetClass.Members.Add(updateMethod);
        }

        private static void AddFields(CodeTypeDeclaration targetClass)
        {
            targetClass.Members.Add(CreateDbField());
        }

        private static CodeMemberField CreateDbField()
        {
            return new CodeMemberField
            {
                Attributes = MemberAttributes.Private,
                Name = "_db",
                Type = new CodeTypeReference("SqlDatabase")
            };
        }
    }
}
