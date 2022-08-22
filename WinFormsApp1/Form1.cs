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
            string msg = $"check1: {checkBox1.Checked}, check2: {checkBox2.CheckState}";
            MessageBox.Show(msg);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            MessageBox.Show(((CheckBox)sender).Checked.ToString());
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            MessageBox.Show(((RadioButton)sender).Checked.ToString());
        }
    }
}