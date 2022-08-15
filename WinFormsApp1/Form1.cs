namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Image image = Properties.Resources.image;
            e.Graphics.DrawImage(image, 100, 100);
        }
    }
}