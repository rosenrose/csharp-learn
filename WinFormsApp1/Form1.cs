namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            dateTimePicker4.CustomFormat = "hh:mm tt yyyy/MM/dd";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                return;
            }

            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value >= 100)
            {
                timer1.Stop();
                progressBar1.Value = 0;
                progressBar2.Value = 0;
                progressBar3.Value = 0;

                return;
            }

            //progressBar1.PerformStep();
            progressBar1.Value++;
            progressBar2.Value++;
            progressBar3.Value++;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            label1.Text = dateTimePicker1.Value.ToString();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            label1.Text = dateTimePicker2.Value.ToShortDateString();
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            label1.Text = dateTimePicker3.Value.ToShortTimeString();
        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            label1.Text = dateTimePicker4.Value.ToLongDateString();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            label1.Text = e.Start.ToLongTimeString();
        }
    }
}