using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace iso2usb
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private String isopath = "";
        private String drive = "";
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            this.isopath = openFileDialog1.FileName;
            textBox1.Text = this.isopath;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drive = listBox1.SelectedItem.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.DriveType == DriveType.Removable)
                {
                    this.listBox1.Items.Add(drive.Name.Replace("\\",""));
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (drive == "" || isopath == "")
            {
                MessageBox.Show("Por favor informe todos campos necessarios");
                return;
            }
            format();
            Sleep(20000U);
            this.progressBar1.Value = 25;
            StringBuilder sb = new StringBuilder();
            sb.Append(drive);
            sb.Append("\\");
            try
            {
                Process.Start("Winrar.exe", string.Format("x {0} {1}",
                  @isopath,
                  sb.ToString())).WaitForExit();

            }
            catch 
            {
            }
            system("bootsect /nt60 " + drive);
            this.progressBar1.Value = 100;
            MessageBox.Show("finished");
        }
        [DllImport("msvcrt.dll")]
        public static extern int system(string format);
        [DllImport("kernel32.dll")]
        static extern void Sleep(uint dwMilliseconds);


        void format()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("format");
            sb.Append(" ");
            sb.Append(this.drive);
            sb.Append(" /FS:FAT32 /X /Q /y");
            system(sb.ToString());
        }
    }
}
