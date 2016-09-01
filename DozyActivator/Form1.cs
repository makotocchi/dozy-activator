using System;
using System.Windows.Forms;
using System.IO;

namespace DozyActivator
{
    public partial class Form1 : Form
    {
        enum DozyState : byte { Off = 6, On };

        public Form1()
        {
            InitializeComponent();
            openFileDialog1.Filter = "TR4 Script Files (*.DAT)|SCRIPT.DAT";
            buttonToggle.Enabled = false;
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxFile.Text = openFileDialog1.FileName;
                if (textBoxFile.Text.ToLower().EndsWith("script.dat"))
                {
                    buttonToggle.Enabled = true;
                }
            }  
        }

        private void buttonToggle_Click(object sender, EventArgs e)
        {
            string fileName = openFileDialog1.FileName;
            DozyState state = toggleDozy(fileName);
            if(state == DozyState.On)
            {
                MessageBox.Show("Dozy on!");
            }
            else
            {
                MessageBox.Show("Dozy off.");
            }
        }

        private static DozyState toggleDozy(string fileName)
        {
            byte[] data = new byte[1];
            using (Stream stream = File.Open(fileName, FileMode.Open))
            {
                stream.Read(data, 0, data.Length);
                if((DozyState) data[0] == DozyState.Off)
                {
                    data[0] = (byte) DozyState.On;
                }
                else
                {
                    data[0] = (byte) DozyState.Off;
                }
                stream.Seek(0, SeekOrigin.Begin);
                stream.Write(data, 0, data.Length);
            }
            return (DozyState) data[0];
        } 
    }
}
