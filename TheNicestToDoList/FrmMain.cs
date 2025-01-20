using System.Windows.Forms;

namespace TheNicestToDoList
{
    public partial class FrmMain : Form
    {
        private ContextMenuStrip cmsTray;

        public FrmMain()
        {
            InitializeComponent();

            //TrayMenu
            cmsTray = new ContextMenuStrip();
            cmsTray.Items.Add("Open", null, OpenApplication);
            cmsTray.Items.Add("Exit", null, ExitApplication);
            ntfIcon.ContextMenuStrip = cmsTray;

            this.Load += FrmMainLoad;
            this.FormClosing += FrmMainFormClosing;
            ntfIcon.DoubleClick += OpenApplication;

        }

        private void FrmMainFormClosing(object? sender, FormClosingEventArgs e)
        {
            if(e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
                ntfIcon.Visible = true;
            }
        }

        private void OpenApplication(object? sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            ntfIcon.Visible = false;
        }

        private void FrmMainLoad(object? sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            ntfIcon.Visible = true;
        }

        private void ExitApplication(object sender, EventArgs e)
        {
            ntfIcon.Visible = false;
            Application.Exit();
        }
    }
}
