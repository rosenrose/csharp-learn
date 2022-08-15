using System.Drawing.Drawing2D;

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
            string str = $"R: {Color.Azure.R} G: {Color.Azure.G} B: {Color.Azure.B} A: {Color.Azure.A}";
            e.Graphics.DrawString(str, Font, Brushes.Black, 100, 100);

            Color color = Color.FromArgb(128, 128, 200);
            using Pen pen = new(color)
            {
                Width = 5,
                DashStyle = DashStyle.DashDot
            };
            e.Graphics.DrawLine(pen, 200, 200, 450, 400);

            Brush brush = new SolidBrush(Color.SkyBlue);
            e.Graphics.FillEllipse(brush, 400, 120, 100, 120);

            brush = new HatchBrush(HatchStyle.Shingle, Color.Red, Color.Blue);
            e.Graphics.FillRectangle(brush, 500, 300, 300, 300);
        }
    }
}