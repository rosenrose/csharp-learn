namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            MessageBox.Show($"{comboBox.SelectedIndex} {comboBox.Text} {comboBox.SelectedItem}");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            MessageBox.Show($"{listBox.SelectedIndex} {listBox.Text} {listBox.SelectedItem}");
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            MessageBox.Show(string.Join('\n', listBox.SelectedItems.Cast<string>()));
        }
    }
}