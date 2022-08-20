namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random rand = new();
            Rectangle rect = new(rand.Next(Width / 3), rand.Next(Height / 3), rand.Next(Width / 2), rand.Next(Height / 2));

            using Graphics g = CreateGraphics();
            g.DrawRectangle(Pens.Chocolate, rect);
        }
    }
}