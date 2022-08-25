namespace WinFormsApp1
{
    public partial class Form2 : Form
    {
        public int a, b;
        public Form2()
        {
            InitializeComponent();

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.TryAgain;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //button1.Text = ((Form1)Owner).msg;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            a = int.TryParse(textBox1.Text, out a) ? a : 0;
            b = int.TryParse(textBox2.Text, out b) ? b : 0;

            Close();
        }
    }
}
