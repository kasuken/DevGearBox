using DevGearBox.Services;
using Microsoft.Extensions.DependencyInjection;
using static DevGearBox.Win.Program;

namespace DevGearBox.Win.Controls
{
    public partial class JsonToYAMLCtl : UserControl
    {
        ToolsServices toolsServices;

        public JsonToYAMLCtl()
        {
            InitializeComponent();

            this.toolsServices = ServiceProviderHolder.ServiceProvider.GetRequiredService<ToolsServices>();
        }

        private void btnCopyToClipboard_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtResult.Text))
            {
                Clipboard.SetText(txtResult.Text);
                MessageBox.Show("The result is in your clipboard.");
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            txtResult.Text = toolsServices.ConvertJsonToYaml(txtSource.Text);
        }
    }
}
