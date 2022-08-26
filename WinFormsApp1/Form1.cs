using Microsoft.Extensions.FileSystemGlobbing;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Dotnet (*.cs, *.xaml)|*.cs;*.xaml|C# (*.cs)|*.cs|XAML (*.xaml)|*.xaml";

            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            foreach (string FileName in openFileDialog1.FileNames)
            {
                if (string.IsNullOrEmpty(FileName))
                {
                    continue;
                }

                MessageBox.Show(File.ReadAllText(FileName));
            }
        }

        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string Path = folderBrowserDialog1.SelectedPath;
            if (string.IsNullOrEmpty(Path))
            {
                return;
            }

            Matcher matcher = new();
            matcher.AddIncludePatterns(new[] { "*.cs", "*.xaml" });

            foreach (string FileName in matcher.GetResultsInFullPath(Path))
            {
                string Content = File.ReadAllText(FileName);

                MessageBox.Show($"{FileName}:\n{Content[..Math.Min(200, Content.Length)]}");
            }

            //foreach (string FileName in new[] { "*.cs", "*.xaml" }.SelectMany(glob => Directory.GetFiles(Path, glob)))
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "C# (*.cs)|*.cs|XAML (*.xaml)|*.xaml";

            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string FileName = saveFileDialog1.FileName;
            if (string.IsNullOrEmpty(FileName))
            {
                return;
            }

            File.WriteAllLines(FileName, new[] { "hello", "zzzzzz" });
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.FullOpen = true;

            if (colorDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            Color color = colorDialog1.Color;
            //using Graphics g = CreateGraphics();

            BackColor = color;
            MessageBox.Show($"R: {color.R} G:{color.G} B:{color.B}");
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
        }

        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pageSetupDialog1.PageSettings = new();
            pageSetupDialog1.ShowDialog();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDialog1.ShowDialog();
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
        }
    }
}