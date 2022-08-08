namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        string MouseCoord;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            MessageBox.Show("Down");
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Click");
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("MouseClick");
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("ButtonDown");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ButtonClick");
        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("ButtonMouseClick");
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            MouseCoord = $"X: {e.X}, Y: {e.Y}";
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString(MouseCoord, Font, Brushes.Magenta, 10, 10);
        }
    }
}