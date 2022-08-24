namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private TextBox[] inputBoxes;

        public Form1()
        {
            InitializeComponent();

            inputBoxes = new TextBox[] { textBox1, textBox2, textBox3 };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ListViewItem Item = new(inputBoxes.Select(box => box.Text).ToArray());

            if (listView1.SelectedItems.Count == 0)
            {
                listView1.Items.Add(Item);
                return;
            }

            listView1.Items.Insert(listView1.SelectedIndices[0], Item);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            foreach (ListViewItem Item in listView1.SelectedItems)
            {
                var SubItems = Item.SubItems;

                foreach (var (textBox, i) in inputBoxes.Select((v, i) => (v, i)))
                {
                    if (i < SubItems.Count)
                    {
                        SubItems[i].Text = textBox.Text;
                        continue;
                    }

                    SubItems.Add(textBox.Text);
                }
            }
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            var SubItems = e.Item.SubItems;

            foreach (var (textBox, i) in inputBoxes.Select((v, i) => (v, i)))
            {
                if (i < SubItems.Count)
                {
                    textBox.Text = SubItems[i].Text;
                    continue;
                }

                textBox.Text = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                return;
            }
            if (MessageBox.Show("Clear?", "Clear", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }

            foreach (ListViewItem Item in listView1.SelectedItems)
            {
                Item.SubItems.Clear();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                return;
            }
            if (MessageBox.Show("Delete?", "Delete", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            foreach (ListViewItem Item in listView1.SelectedItems)
            {
                listView1.Items.Remove(Item);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count == 0)
            {
                return;
            }
            if (MessageBox.Show("Purge?", "Purge", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            listView1.Items.Clear();
        }
    }
}