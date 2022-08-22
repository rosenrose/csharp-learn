namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            label1.Text = $"x: {e.NewValue}";
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            label2.Text = $"y: {e.NewValue}";
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label3.Text = ((TrackBar)sender).Value.ToString();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            label3.Text = ((NumericUpDown)sender).Value.ToString();
        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            DomainUpDown upDown = (DomainUpDown)sender;
            label3.Text = $"{upDown.SelectedIndex}, text: {upDown.Text}, item: {upDown.SelectedItem}";
        }
    }
}