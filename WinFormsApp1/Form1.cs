namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Open");
        }

        private void optionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Option");
        }
    }
}