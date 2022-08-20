namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        BufferedGraphicsContext ctx;
        BufferedGraphics graphics;
        Image image;
        public Form1()
        {
            InitializeComponent();

            ctx = BufferedGraphicsManager.Current;
            image = Properties.Resources.image;
            ctx.MaximumBuffer = new(image.Width * 2, image.Height * 2);
            graphics = ctx.Allocate(CreateGraphics(), new(0, 0, image.Width * 2, image.Height * 2));
            graphics.Graphics.Clear(Color.Blue);
            SetClientSizeCore(image.Width * 2, image.Height * 2);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Random rand = new();
            for (int i = 0; i < 10; i++)
            {
                graphics.Graphics.DrawImage(image, rand.Next(image.Width), rand.Next(image.Height));
            }
            graphics.Render(e.Graphics);
        }
    }
}