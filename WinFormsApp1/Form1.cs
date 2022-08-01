namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            button1.Text = "Hello";
            MessageBox.Show("Hi");
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //File.WriteAllText("log.txt", e.ClipRectangle.Width.ToString());
            e.Graphics.DrawString("abc", Font, Brushes.Aqua, 1, 2);
            e.Graphics.DrawLine(Pens.Red, 10, 10, 200, 20);
            e.Graphics.DrawEllipse(Pens.SkyBlue, 10, 20, 50, 80);
            e.Graphics.DrawRectangle(Pens.OliveDrab, 100, 30, 90, 80);
        }
    }
}