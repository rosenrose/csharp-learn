namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        List<Rectangle> Rects = new();
        Point InitPoint;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Rectangle rect = new()
            {
                X = e.X,
                Y = e.Y
            };
            Rects.Add(rect);
            (InitPoint.X, InitPoint.Y) = (e.X, e.Y);

            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (Rects.Count == 0)
            {
                return;
            }

            string str = $"left: {Rects[^1].Left}, top: {Rects[^1].Top}, right: {Rects[^1].Right}, bottom: {Rects[^1].Bottom}, " +
                         $"width: {Rects[^1].Width}, height: {Rects[^1].Height}";

            e.Graphics.DrawString(str, Font, Brushes.Coral, 10, 10);

            foreach (var rect in Rects)
            {
                e.Graphics.DrawRectangle(Pens.Chocolate, rect);
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            if (Rects.Count == 0)
            {
                return;
            }

            var rect = Rects[^1];   //Rects[^1].Width = 0; 불가능

            (rect.Width, rect.Height) = (Math.Abs(e.X - InitPoint.X), Math.Abs(e.Y - InitPoint.Y));

            if (e.X < InitPoint.X)
            {
                rect.X = e.X;
            }
            if (e.Y < InitPoint.Y)
            {
                rect.Y = e.Y;
            }

            Rects[^1] = rect;

            Invalidate();
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            //Invalidate();
        }
    }
}