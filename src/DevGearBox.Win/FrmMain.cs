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
    }
}
