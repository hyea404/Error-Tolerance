using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Windows.Forms;


namespace Error_Tolerance
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Event handler ini tidak memiliki implementasi khusus.
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            // Event handler ini tidak memiliki implementasi khusus.
        }

        private double totalErrorTolerance = 0;
        private int fileCount = 0;

        private void buttonCalculateErrorTolerance_Click(object sender, EventArgs e)
        {
            dataGridViewResults.Rows.Clear();

            foreach (string fileName in listBoxFiles.Items)
            {
                try
                {
                    string[] lines = File.ReadAllLines(fileName);
                    int komputasiCount = 0;
                    int kontrolCount = 0;
                    int errorHandlerCount = 0;

                    foreach (string line in lines)
                    {
                        if (line.TrimStart().StartsWith("//"))
                            continue;

                        if (line.Contains("//"))
                        {
                            string codeLine = line.Split(new[] { "//" }, StringSplitOptions.None)[0].Trim();
                            if (string.IsNullOrEmpty(codeLine))
                                continue;
                        }

                        if (line.Contains("+") || line.Contains("-") || line.Contains("*") || line.Contains("/"))
                        {
                            komputasiCount++;
                        }

                        if (line.Contains("if") || line.Contains("for") || line.Contains("while") || line.Contains("switch"))
                        {
                            kontrolCount++;
                        }

                        if (line.Contains("catch"))
                        {
                            errorHandlerCount++;
                        }
                    }

                    double persentaseErrorTolerance = CalculateErrorTolerance(komputasiCount, kontrolCount, errorHandlerCount);

                    totalErrorTolerance += persentaseErrorTolerance;
                    fileCount++;
                    // Menampilkan hasil analisis dalam textBox3
                    //textBox3.Text = "Computations: " + komputasiCount + Environment.NewLine
                    //+ "Control Structure: " + kontrolCount + Environment.NewLine
                    //+ "Error Handler: " + errorHandlerCount + Environment.NewLine
                    //+ "Error Tolerance: " + persentaseErrorTolerance.ToString("0.00") + "%";

                    dataGridViewResults.Rows.Add(fileName, komputasiCount, kontrolCount, errorHandlerCount, persentaseErrorTolerance);
                }


                catch (FileNotFoundException)
                {
                    // Handle file not found exception
                    MessageBox.Show($"File {fileName} not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (fileCount > 0)
            {
                double averageErrorTolerance = totalErrorTolerance / fileCount;
                textBox3.Text="Average Error Tolerance:" + averageErrorTolerance.ToString("0.00") + "%";
            }

        }

        private double CalculateErrorTolerance(int komputasiCount, int kontrolCount, int errorHandlerCount)
        {
            // Implementasikan rumus error tolerance sesuai kebutuhan


            if (komputasiCount + kontrolCount == 0)
            {
                return 0.0;
            }

            return ((double)errorHandlerCount / (komputasiCount + kontrolCount)) * 100.0;
        }


        private void buttonBrowseFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = GetFileFilter(); // Dapatkan filter file sesuai dengan pilihan bahasa pemrograman
                openFileDialog.Multiselect = true;


                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = openFileDialog.FileName;
                    listBoxFiles.Items.Clear(); // Hapus item sebelumnya dari ListBox
                    listBoxFiles.Items.Add(openFileDialog.FileName); // Tampilkan file yang dipilih di ListBox
                }
            }
        }

        private void buttonBrowseFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    listBoxFiles.Items.Clear();
                    string[] files = Directory.GetFiles(folderBrowserDialog.SelectedPath, GetFileExtensionFilter());
                    listBoxFiles.Items.AddRange(files);

                }
            }
        }

        private string GetFileFilter()
        {
            if (radioButtonCSharp != null && radioButtonCSharp.Checked)
                return "C# Files (*.cs)|*.cs";
            else if (radioButtonCpp != null && radioButtonCpp.Checked)
                return "C++ Files (*.cpp)|*.cpp";
            else if (radioButtonJava != null && radioButtonJava.Checked)
                return "Java Files (*.java)|*.java";
            else if (radioButtonPython != null && radioButtonPython.Checked)
                return "Python Files (*.py)|*.py";
            else
                return "All Files (*.cs, *.cpp, *.java, *.py)|*.cs;*.cpp;*.java;*.py";
        }

        private string GetFileExtensionFilter()
        {
            if (radioButtonCSharp.Checked)
                return "*.cs";
            else if (radioButtonCpp.Checked)
                return "*.cpp";
            else if (radioButtonJava.Checked)
                return "*.java";
            else if (radioButtonPython.Checked)
                return "*.py";
            else
                return "*.*"; // Jika memilih "All Files", tampilkan semua ekstensi
        }


        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            // Event handler ini tidak memiliki implementasi khusus.
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridViewResults.Columns.Add("FileName", "File Location");
            dataGridViewResults.Columns.Add("Computations", "Computations");
            dataGridViewResults.Columns.Add("ControlStructures", "Control Structures");
            dataGridViewResults.Columns.Add("ErrorHandlers", "Error Handlers");
            dataGridViewResults.Columns.Add("ErrorTolerance", "Error Tolerance");
        }

        private void listBoxFiles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            ShowHelpMessageBox();
        }

        private void ShowHelpMessageBox()
        {
            string helpMessage = GenerateHelpMessage();
            MessageBox.Show(helpMessage, "User Guide", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string GenerateHelpMessage()
        {
            StringBuilder helpMessage = new StringBuilder();

            helpMessage.AppendLine("User Guide for Error Tolerance Application");
            helpMessage.AppendLine();
            helpMessage.AppendLine("1. Browse your file.");
            helpMessage.AppendLine("2. Click 'Calculate' to run testing.");
            helpMessage.AppendLine("3. The Apllication will show the result.");
            helpMessage.AppendLine("4. Result will contain .");


            return helpMessage.ToString();
        }

        //Reset button
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox3.Clear();
            listBoxFiles.Items.Clear();
            totalErrorTolerance = 0;
            fileCount = 0;
            dataGridViewResults.Rows.Clear();

        }


        private void radioButtonJava_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonCpp_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonCSharp_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonPython_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewResults_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
