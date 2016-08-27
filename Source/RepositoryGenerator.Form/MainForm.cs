using Microsoft.Practices.Unity;
using RepositoryGenerator.Core;
using RepositoryGenerator.Core.Generators;
using RepositoryGenerator.Core.Generators.Interfaces;
using RepositoryGenerator.Core.Mappers;
using RepositoryGenerator.Core.Repositories;
using RepositoryGenerator.Core.Repositories.Interfaces;

namespace RepositoryGenerator.Form
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {

        }

        private void btnGenerate_Click(object sender, System.EventArgs e)
        {
            var container = CoreDependencyBuilder.Create();

            var tableDefinition = container.Resolve<ITableDefinitionRepository>().Load(txtTableName.Text.Trim());

            var sqlCommandGenerator = container.Resolve<ISqlCommandGenerator>();

            var insertCommand = sqlCommandGenerator.CreateForInsert(tableDefinition);

            var selectCommand = sqlCommandGenerator.CreateForSelect(tableDefinition);

            txtResult.Text = insertCommand;
        }
    }
}
