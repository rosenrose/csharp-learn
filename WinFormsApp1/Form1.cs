namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private Form2? Modeless = null;
        public string msg = "hello";

        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using Form2 Modal = new() { Owner = this };

            msg = textBox1.Text;

            textBox1.Text = $"{Modal.ShowDialog()} {Modal.a + Modal.b}";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Modeless != null)
            {
                return;
            }

            Modeless = new() { Owner = this };
            Modeless.FormClosing += (object? sender, FormClosingEventArgs e) =>
            {
                textBox1.Text = $"{Modeless.DialogResult} {Modeless.a + Modeless.b}";
                Modeless = null;
            };

            msg = textBox1.Text;
            Modeless.Show();
        }
    }
}