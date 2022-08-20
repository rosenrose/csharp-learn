namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Size = new(640, 480);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            //Control control = (Control)sender;
            //control.Size = new(500, 500);

            Size = new(500, 500);
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            //Size = new(500, 500);
        }

        private void Form1_Layout(object sender, LayoutEventArgs e)
        {
            MessageBox.Show("layout");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button1.Visible)
            {
                button1.Hide();
            }
            else
            {
                button1.Show();
            }
        }
    }
}