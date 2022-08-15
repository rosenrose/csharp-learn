namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        int width = 100;
        int height = 100;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Rectangle rect = new(e.X - width / 2, e.Y - height / 2, width, height);

            using Graphics g = CreateGraphics();
            g.DrawEllipse(Pens.Chocolate, rect);
        }
    }
}