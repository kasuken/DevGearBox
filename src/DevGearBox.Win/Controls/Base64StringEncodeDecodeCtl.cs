using DevGearBox.Services;
using Microsoft.Extensions.DependencyInjection;
using static DevGearBox.Win.Program;

namespace DevGearBox.Win.Controls
{
    public partial class Base64StringEncodeDecodeCtl : UserControl
    {
        ToolsServices toolsServices;

        public Base64StringEncodeDecodeCtl()
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

        private void btnEncode_Click(object sender, EventArgs e)
        {
            txtResult.Text = toolsServices.EncodeBase64(txtSource.Text);
        }

        private void btnDecode_Click(object sender, EventArgs e)
        {
            try
            {
                txtResult.Text = toolsServices.DecodeBase64(txtSource.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error decoding Base64 string: {ex.Message}");
            }
        }
    }
}
