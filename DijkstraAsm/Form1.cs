using System.IO;
using System.Security;
using static System.Net.Mime.MediaTypeNames;

namespace DijkstraAsm
{
    public partial class Form1 : Form
    {
        private string _path;
        public Form1()
        {
            InitializeComponent();
        }

        private void chooseFileButton_Click(object sender, EventArgs e)
        {
            string test = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    inputPathTextBox.Text = openFileDialog1.FileName;
                    var sr = new StreamReader(openFileDialog1.FileName);
                    test = sr.ReadToEnd();
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            try
            {
                int nodesCount = int.Parse(txtNodesCount.Text);
                int minConnections = int.Parse(txtMinConn.Text);
                int weightsRange = int.Parse(txtWightsRange.Text);
                GraphGeneration.GenerateGraphFile(nodesCount, minConnections, weightsRange, _path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error occured");
            }
        }

        private void chooseDirectoryButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _path = saveFileDialog1.FileName;
                tbPath.Text = _path;
            }
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            try
            {
                string inputPath = inputPathTextBox.Text;
                Graph graph = new Graph(inputPath);
                var path = Algorithm.GetShortestPath(graph);
                MessageBox.Show($"Start: {graph.StartNode}, End: {graph.EndNode}\n" + "Path: " + string.Join(" -> ", path), "Results");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error occured");
            }
        }
    }
}
