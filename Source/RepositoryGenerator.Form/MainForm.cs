using RepositoryGenerator.Core.Generators;
using RepositoryGenerator.Core.Mappers;
using RepositoryGenerator.Core.Repositories;

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
            var tableDefinitionRepository = new TableDefinitionRepository(new SqlDataTypeMapper());

            var tableDefinition = tableDefinitionRepository.Load(txtTableName.Text.Trim());


            var sqlCommandGenerator = new SqlCommandGenerator();

            var insertCommand = sqlCommandGenerator.CreateForInsert(tableDefinition);

            txtResult.Text = insertCommand;
        }
    }
}
