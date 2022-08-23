namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox2.Image = Properties.Resources.image2;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.image2;
        }
    }
}