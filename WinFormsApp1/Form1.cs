namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        Image? image;
        Form2 form2 = new();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            form2.Show();
            image = Properties.Resources.image1;
            AutoScrollMinSize = image.Size;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(image!, AutoScrollPosition.X, AutoScrollPosition.Y);
            (form2.a, form2.b) = (AutoScrollPosition.X, AutoScrollPosition.Y);
            form2.Invalidate();
        }
    }
}