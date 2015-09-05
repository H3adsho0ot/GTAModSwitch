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

namespace GTAModSwitch
{
    public partial class main : Form
    {
        private string mTempPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\GTAModSwitch\Save";


        public main()
        {
            InitializeComponent();
        }

        private void main_Load(object sender, EventArgs e)
        {
            listBox1.AllowDrop = true;

            if (!Directory.Exists(mTempPath))
            {
                Directory.CreateDirectory(mTempPath);
            }
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] filePaths = (string[])(e.Data.GetData(DataFormats.FileDrop));
                foreach (string fileLoc in filePaths)
                {
                    if (File.Exists(fileLoc))
                    {
                        listBox1.Items.Add(fileLoc);
                    }
                    else if (Directory.Exists(fileLoc))
                    {
                        addDirectory(fileLoc);
                    }

                }
            }
        }

        private void addDirectory(string aPath)
        {
            foreach (string file in Directory.GetFiles(aPath))
            {
                if (File.Exists(file))
                {
                    listBox1.Items.Add(file);
                }
            }

            foreach (string directory in Directory.GetDirectories(aPath))
            {
                if (Directory.Exists(directory))
                {
                    addDirectory(directory);
                }
            }
        }

        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private string getFilename(string aFullPath)
        {
            string[] pathSplit = aFullPath.Split('\\');
            string fileName = pathSplit[pathSplit.Count() - 1];
            return fileName;
        }

        private void enableMods()
        {

        }

        private void disableMods()
        {
            foreach (string file in listBox1.Items)
            {
                if (File.Exists(file))
                {
                    string newFilePath = mTempPath + "\\" + getFilename(file);
                    File.Move(file, newFilePath);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            disableMods();
        }
    }
}
