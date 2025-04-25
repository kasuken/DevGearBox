using DevGearBox.Services;
using Microsoft.Extensions.DependencyInjection;
using static DevGearBox.Win.Program;

namespace DevGearBox.Win.Controls
{
    public partial class JsonFormatterCtl : UserControl
    {
        ToolsServices toolsServices;

        public JsonFormatterCtl()
        {
            InitializeComponent();

            this.toolsServices = ServiceProviderHolder.ServiceProvider.GetRequiredService<ToolsServices>();
        }

        private void JsonFormatterCtl_Load(object sender, EventArgs e)
        {

        }

        private void btnFormatJSON_Click(object sender, EventArgs e)
        {
            txtResult.Text = toolsServices.FormatJson(txtSource.Text);
        }

        private void JsonFormatterCtl_Click(object sender, EventArgs e)
        {
        }

        private void btnCopyToClipboard_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtResult.Text))
            {
                Clipboard.SetText(txtResult.Text);
                MessageBox.Show("The result is in your clipboard.");
            }
        }

        private void btnValidateJSON_Click(object sender, EventArgs e)
        {
            if (toolsServices.ValidateJson(txtSource.Text))
            {
                MessageBox.Show("JSON is valid.");
            }
            else
            {
                MessageBox.Show("JSON is not valid.");
            }
        }
    }
}
