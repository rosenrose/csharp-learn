namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        List<Point> Points = new();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (Points.Count >= 3)
            {
                Points.Clear();
            }

            Points.Add(new(e.X, e.Y));
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (Points.Count < 3)
            {
                return;
            }

            for (int i = 0; i < Points.Count; i++)
            {
                e.Graphics.DrawLine(Pens.BlueViolet, Points[i], Points[(i + 1) % 3]);
            }
        }
    }
}