using NAudio.Wave;
using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
namespace WindowsFormsApp12
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            FunctionsAndData.CleanUp();
        }

        [STAThread]
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "JSON files (*.json)|*.json";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                {
                    string jsonContent = reader.ReadToEnd();
                    FunctionsAndData.SetTrack(jsonContent);
                    label1.Text = "Название: " + ThisTrack.name;
                    label2.Text = "Описание: " + ThisTrack.description;

                    if (!string.IsNullOrEmpty(ThisTrack.IconFilePath) && File.Exists(ThisTrack.IconFilePath))
                    {
                        pictureBox1.Image = Image.FromFile(ThisTrack.IconFilePath);
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom; 
                    }
                    else
                    {
                        pictureBox1.Image = null; 
                    }
                    FunctionsAndData.SetTrack(jsonContent);

                    FunctionsAndData.PlayMusic(ThisTrack.MusicFilePath);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (FunctionsAndData.outputDevice != null)
            {
                if (FunctionsAndData.outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    FunctionsAndData.outputDevice.Pause();
                    button3.Text = "Продолжить";
                }
                else if (FunctionsAndData.outputDevice.PlaybackState == PlaybackState.Paused)
                {
                    FunctionsAndData.outputDevice.Play();
                    button3.Text = "Пауза";
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FunctionsAndData.CleanUp();

            ThisTrack.name = "unknown";
            ThisTrack.MusicFilePath = string.Empty;
            ThisTrack.IconFilePath = "base.png";
            ThisTrack.description = "unknown";
            pictureBox1.Image = null;
            label1.Text =  ThisTrack.name;
            label2.Text =  ThisTrack.description;
        }

        private void pictureBox1_Click(object sender, EventArgs e) { }
    }
}
