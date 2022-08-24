namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"{tabControl1.SelectedIndex} {tabControl1.SelectedTab}");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"{tabControl1.SelectedIndex} {tabControl1.SelectedTab}");
        }
    }
}