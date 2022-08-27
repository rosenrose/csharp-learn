namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void form2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form2() { MdiParent = this, message = DateTime.Now.ToLongTimeString() }.Show();
        }
    }
}