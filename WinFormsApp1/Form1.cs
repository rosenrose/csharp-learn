namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        Bitmap bitmap;
        public Form1()
        {
            InitializeComponent();

            bitmap = new(400, 300);
            SetClientSizeCore(400, 300);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Image imgage = Image.FromFile("image.png");
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.Yellow);

            graphics.DrawString("C#", Font, Brushes.Black, 10, 10);
            graphics.DrawRectangle(Pens.Red, 100, 10, 200, 100);

            e.Graphics.DrawImage(bitmap, 0, 0);
        }
    }
}