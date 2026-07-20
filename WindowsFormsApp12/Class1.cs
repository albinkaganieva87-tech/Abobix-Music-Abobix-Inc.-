using NAudio.Wave;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
namespace WindowsFormsApp12
{
    public  class Track
    {
        public  string name { get; set; }
        public  string description { get; set; }
        public  string MusicFilePath { get; set; }
        public  string IconFilePath { get; set; }


    }
    public static class ThisTrack
    {
        public static string name { get; set; }
        public static string description { get; set; }
        public static string MusicFilePath { get; set; }
        public static string IconFilePath { get; set; }


    }

    public static class FunctionsAndData
    {

            public static WaveOutEvent outputDevice;
            public static AudioFileReader audioFile;

            public static void SetTrack(string jsonContent)
            {
                var track = JsonConvert.DeserializeObject<Track>(jsonContent);
                if (track != null)
                {
                    ThisTrack.name = track.name;
                    ThisTrack.description = track.description;
                    ThisTrack.MusicFilePath = track.MusicFilePath;
                    ThisTrack.IconFilePath = track.IconFilePath;
                }
            }

            public static void PlayMusic(string filepath)
            {
                CleanUp();

                if (string.IsNullOrEmpty(filepath) || !File.Exists(filepath))
                {
                    System.Windows.Forms.MessageBox.Show("Аудиофайл не найден по пути: " + filepath);
                    return;
                }

                try
                {
                    outputDevice = new WaveOutEvent();
                    audioFile = new AudioFileReader(filepath);

                    outputDevice.Init(audioFile);
                    outputDevice.Play();
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Ошибка воспроизведения: " + ex.Message);
                }
            }

            public static void CleanUp()
            {
                if (outputDevice != null)
                {
                    outputDevice.Stop();
                    outputDevice.Dispose();
                    outputDevice = null;
                }
                if (audioFile != null)
                {
                    audioFile.Dispose();
                    audioFile = null;
                }
            }
            
        }
}

