using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using static System.Drawing.Color;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace DCP_Core
{
    public partial class Form1 : Form
    {
        private static RegistryKey DCPre = Registry.CurrentUser.OpenSubKey("Software\\Despair Codec Pack");
        private String DCPVersion = DCPre.GetValue("Version").ToString();
        private String DCPPath = DCPre.GetValue("InstallPath").ToString();


        public Form1()
        {
            InitializeComponent();
        }

        private void rundll32(String relativePath, String dll, String args)
        {
            String path = DCPPath + relativePath;
            ProcessStartInfo startInfo = new ProcessStartInfo();
            //startInfo.Verb = "RunAs";
            startInfo.FileName = "rundll32";
            startInfo.Arguments = "\"" + path + dll + "\"" + "," + args;
            var process = Process.Start(startInfo);
            process.WaitForExit();
        }

        private void runPrgm(String relativePath, String exe, String args="", bool waitForExit=true)
        {
            String path = DCPPath + relativePath;
            ProcessStartInfo startInfo = new ProcessStartInfo();
            //startInfo.Verb = "RunAs";
            startInfo.FileName = DCPPath + relativePath + exe;
            startInfo.Arguments = args;
            var process = Process.Start(startInfo);
            if (waitForExit) process.WaitForExit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // LAV Splitter
            String path = "\\LAVFilters\\";
            String file = "LAVSplitter.ax";
            String args = "OpenConfiguration";
            rundll32(path, file, args);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // LAV Video
            String path = "\\LAVFilters\\";
            String file = "LAVVideo.ax";
            String args = "OpenConfiguration";
            rundll32(path, file, args);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // LAV Audio
            String path = "\\LAVFilters\\";
            String file = "LAVAudio.ax";
            String args = "OpenConfiguration";
            rundll32(path, file, args);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // XySubFilter
            String path = "\\XySubFilter\\";
            String file = "XySubFilter.dll";
            String args = "XySubFilterConfiguration";
            rundll32(path, file, args);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://despair-paradise.com/dcp");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // Associate Files
            String path = "\\MPC-BE\\";
            String file = "mpc-be64.exe";
            String args = "/adminoption 10027";
            runPrgm(path, file, args);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // MadVR
            String path = "\\madVR\\";
            String file = "madHcCtrl.exe";
            String args = "";
            runPrgm(path, file, args, false);
            MessageBox.Show("Right click on MadVR icon in System Tray");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // Presets
            String path = "\\Config\\";
            String file = "DCP-Settings.exe";
            runPrgm(path, file);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Reinstall
            String path = "\\";
            String file = "install.bat";
            runPrgm(path, file);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Check for Update
            string url = "http://files.automne.me/dcp/lastVersion";
            string contents;
            using (var wc = new System.Net.WebClient())
                contents = wc.DownloadString(url);
            contents = contents.Remove(contents.Length - 1);
            if (contents == DCPVersion) MessageBox.Show("You are using the latest version !\nNo update required.");
            else
            {
                MessageBox.Show("A new release is available.               \nCurrent Version :   " + DCPVersion + "\nNew Version :        " +
                                contents);
                System.Diagnostics.Process.Start("http://despair-paradise.com/dcp");

            }
        }



        private void PaintBorderlessGroupBox(object sender, PaintEventArgs p)
        {
            GroupBox box = (GroupBox)sender;
            p.Graphics.Clear(SystemColors.Control);
            var brush = new SolidBrush(Color.FromArgb(255, 41, 47, 51));
            p.Graphics.DrawString(box.Text, box.Font, brush, 0, 0);
        }

    }
}
