using System;
using System.IO;
using System.Text;
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

        private double persentaseErrorTolerance;


        private void button1_Click(object sender, EventArgs e)
        {
            // Tombol "Analyze" digunakan untuk menganalisis file yang telah dipilih.

            string fileName = textBox1.Text;

            try
            {
                string[] lines = File.ReadAllLines(fileName);
                int komputasiCount = 0;
                int kontrolCount = 0;
                int errorHandlerCount = 0;

                foreach (string line in lines)
                {
                    // Memeriksa apakah baris mengandung komentar satu baris
                    if (line.TrimStart().StartsWith("//"))
                        continue;

                    // Mengabaikan komentar dalam banyak bahasa pemrograman
                    if (line.Contains("//"))
                    {
                        string codeLine = line.Split(new[] { "//" }, StringSplitOptions.None)[0].Trim();
                        if (string.IsNullOrEmpty(codeLine))
                            continue;
                    }

                    // Menghitung komputasi
                    if (line.Contains("+") || line.Contains("-") || line.Contains("*") || line.Contains("/"))
                    {
                        komputasiCount++;
                    }

                    // Menghitung struktur kontrol
                    if (line.Contains("if") || line.Contains("for") || line.Contains("while") || line.Contains("switch"))
                    {
                        kontrolCount++;
                    }

                    // Menghitung error handler
                    if (line.Contains("catch"))
                    {
                        errorHandlerCount++;
                    }
                }

                // Hitung persentase error tolerance
                double persentaseErrorTolerance = (double)errorHandlerCount / (komputasiCount + kontrolCount) * 100;

                // Menampilkan hasil analisis dalam textBox3
                textBox3.Text = "Computations: " + komputasiCount + Environment.NewLine
                            + "Control Structure: " + kontrolCount + Environment.NewLine
                            + "Error Handler: " + errorHandlerCount + Environment.NewLine
                            + "Error Tolerance: " + persentaseErrorTolerance.ToString("0.00") + "%";
            }
            catch (FileNotFoundException)
            {
                textBox3.Text = "File tidak ditemukan.";
            }
            catch (Exception ex)
            {
                textBox3.Text = "Terjadi kesalahan: " + ex.Message;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Tombol "Browse" digunakan untuk memilih file yang akan dianalisis.
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // Tidak ada filter file yang ditetapkan
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = openFileDialog.FileName;
                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            // Event handler ini tidak memiliki implementasi khusus.
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Tombol "Browse Folder" digunakan untuk memilih folder yang akan dianalisis.
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    string folderPath = folderBrowserDialog.SelectedPath;

                    try
                    {
                        // Mendapatkan daftar file dalam folder
                        string[] files = Directory.GetFiles(folderPath);

                        // Analisis setiap file dalam folder
                        int totalComputations = 0;
                        int totalControlStructures = 0;
                        int totalErrorHandlers = 0;

                        foreach (string file in files)
                        {
                            try
                            {
                                string[] lines = File.ReadAllLines(file);
                                int komputasiCount = 0;
                                int kontrolCount = 0;
                                int errorHandlerCount = 0;

                                foreach (string line in lines)
                                {
                                    // Memeriksa apakah baris mengandung komentar satu baris
                                    if (line.TrimStart().StartsWith("//"))
                                        continue;

                                    // Mengabaikan komentar dalam banyak bahasa pemrograman
                                    if (line.Contains("//"))
                                    {
                                        string codeLine = line.Split(new[] { "//" }, StringSplitOptions.None)[0].Trim();
                                        if (string.IsNullOrEmpty(codeLine))
                                            continue;
                                    }

                                    // Menghitung komputasi
                                    if (line.Contains("+") || line.Contains("-") || line.Contains("*") || line.Contains("/"))
                                    {
                                        komputasiCount++;
                                    }

                                    // Menghitung struktur kontrol
                                    if (line.Contains("if") || line.Contains("for") || line.Contains("while") || line.Contains("switch"))
                                    {
                                        kontrolCount++;
                                    }

                                    // Menghitung error handler
                                    if (line.Contains("catch"))
                                    {
                                        errorHandlerCount++;
                                    }
                                }

                                // Menambahkan hasil analisis file ke total
                                totalComputations += komputasiCount;
                                totalControlStructures += kontrolCount;
                                totalErrorHandlers += errorHandlerCount;
                            }
                            catch (Exception ex)
                            {
                                // Handle error ketika membaca file
                                textBox3.Text += "Error saat membaca file '" + file + "': " + ex.Message + Environment.NewLine;
                            }
                        };

                        // Hitung persentase error tolerance untuk keseluruhan folder
                        double persentaseErrorTolerance = (double)totalErrorHandlers / (totalComputations + totalControlStructures) * 100;

                        // Menampilkan hasil analisis keseluruhan folder dalam textBox3
                        textBox3.Text = " Computations: " + totalComputations + Environment.NewLine
                                    + " Control Structures: " + totalControlStructures + Environment.NewLine
                                    + " Error Handlers: " + totalErrorHandlers + Environment.NewLine
                                    + " Error Tolerance: " + persentaseErrorTolerance.ToString("0.00") + "%";
                    }
                    catch (Exception ex)
                    {
                        // Handle error ketika membaca folder
                        textBox3.Text = "Error saat membaca folder: " + ex.Message;
                    }
                }
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
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
    }
}
