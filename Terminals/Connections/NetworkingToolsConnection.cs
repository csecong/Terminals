namespace Terminals.Connections
{
    using Configuration.Files.Main.Favorites;
    using Connection;
    using Kohl.Framework.Logging;
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class NetworkingToolsConnection : ConnectionBase
    {
        private bool connected;
        private NetworkingToolsLayout networkingToolsLayout1;

        public NetworkingToolsConnection()
        {
            this.InitializeComponent();
        }

        public override bool Connected
        {
            get { return this.connected; }
        }

        protected override void ChangeDesktopSize(DesktopSize desktopSize, System.Drawing.Size size)
        {
        }

        public override bool Connect()
        {
            this.connected = false;
            this.networkingToolsLayout1.OnTabChanged += this.networkingToolsLayout1_OnTabChanged;
            this.networkingToolsLayout1.Parent = this.TerminalTabPage;
            this.Parent = this.TerminalTabPage;
            return this.connected = true;
        }

        private void networkingToolsLayout1_OnTabChanged(object sender, TabControlEventArgs e)
        {
            this.TerminalTabPage.Title = e.TabPage.Text;
        }

        public override void Disconnect()
        {
            if (!connected)
                return;

            try
            {
                this.connected = false;
                this.networkingToolsLayout1.Dispose();

                this.CloseTabPage();
            }
            catch (Exception ex)
            {
                Log.Error("Unable to disconnect from the Network Tools.", ex);
            }

            InvokeIfNecessary(() => base.Disconnect());
        }

        private void InitializeComponent()
        {
            this.networkingToolsLayout1 = new NetworkingToolsLayout();
            this.SuspendLayout();
            this.networkingToolsLayout1.Location = new Point(0, 0);
            this.networkingToolsLayout1.Size = new Size(700, 500);
            this.networkingToolsLayout1.TabIndex = 0;
            this.networkingToolsLayout1.Dock = DockStyle.Fill;
            this.ResumeLayout(false);
        }

        public void Execute(string Action, string Host)
        {
            this.networkingToolsLayout1.Execute(Action, Host);
        }
    }
}