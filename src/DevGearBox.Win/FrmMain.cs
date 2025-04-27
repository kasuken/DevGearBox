using DevGearBox.Services;
using DevGearBox.Win.Controls;

namespace DevGearBox.Win
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            // Set the default control to be displayed
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(new JsonFormatterCtl());
        }

        private void btnJSONFormatter_Click(object sender, EventArgs e)
        {
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(new JsonFormatterCtl());
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(new AboutCtl());
        }

        private void btnJSONtoYAML_Click(object sender, EventArgs e)
        {
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(new JsonToYAMLCtl());
        }

        private void btnBase64String_Click(object sender, EventArgs e)
        {
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(new Base64StringEncodeDecodeCtl());
        }
    }
}
