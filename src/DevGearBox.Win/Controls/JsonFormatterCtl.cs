using DevGearBox.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            txtResult.Text = toolsServices.FormaJson(txtSource.Text);
        }

        private void JsonFormatterCtl_Click(object sender, EventArgs e)
        {
            if (toolsServices.ValidateJson(txtResult.Text))
            {
                MessageBox.Show("JSON is valid.");
            }
            else
            {
                MessageBox.Show("JSON is not valid.");
            }
        }

        private void btnCopyToClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtResult.Text);
            MessageBox.Show("The result is in your clipboard.");
        }
    }
}
