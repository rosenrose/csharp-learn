namespace WinFormsApp1
{
    public partial class Form2 : Form
    {
        public int a, b;
        public string? message;

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
            button1.Text = message;
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            (textBox1.Text, textBox2.Text) = (a.ToString(), b.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            a = int.TryParse(textBox1.Text, out a) ? a : 0;
            b = int.TryParse(textBox2.Text, out b) ? b : 0;

            Close();
        }
    }
}
