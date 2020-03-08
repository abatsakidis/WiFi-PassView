using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace WiFi_Passview
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox1.Text += "[X] Checking for wireless interface...\r\n";
            
            Process proc = null;
            try
            {
                string batDir = string.Format(@Application.StartupPath + "\\Include\\");
                proc = new Process();
                proc.StartInfo.WorkingDirectory = batDir;
                proc.StartInfo.FileName = "wifipassview.bat";
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                proc.WaitForExit();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            textBox1.Text += "[X] Extracting passwords and saving it...\r\n";

            Process proc2 = null;
            try
            {
                string batDir = string.Format(@Application.StartupPath + "\\Include\\");
                proc2 = new Process();
                proc2.StartInfo.WorkingDirectory = batDir;
                proc2.StartInfo.FileName = "wifi-scan.bat";
                proc2.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                proc2.StartInfo.CreateNoWindow = true;
                proc2.Start();
                proc2.WaitForExit();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


            textBox1.Text += "[X] Done...\r\n";
            textBox1.Text += "[X] ======================================\r\n";
            textBox1.Text += File.ReadAllText(@Application.StartupPath + "\\Include\\creds.txt");

            try
            {
                File.Delete(@Application.StartupPath + "\\Include\\creds.txt");
            }
            catch (IOException ioExp)
            {
                MessageBox.Show(ioExp.ToString());
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Environment.Exit(0);
            }
            else
            {
                // e.Cancel = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {

               
                TextWriter tw = new StreamWriter(folderBrowserDialog1.SelectedPath + "\\wifi-creds.txt");
                tw.WriteLine(textBox1.Text);
                tw.Close();
                MessageBox.Show("Saved to " + folderBrowserDialog1.SelectedPath + "\\wifi-creds.txt", "Saved WiFi Creds File", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    
    }
}
