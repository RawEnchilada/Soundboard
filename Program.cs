using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;



namespace Soundboard_forms
{
    static class Program
    {     
        [STAThread]
        static void Main()
        {       

            audio = new Audio();
            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1.window = new Form1();
            Application.Run(Form1.window);
        }


        public static Audio audio = null;
        
    }





    public partial class Audio
    {
        public bool recording { get; private set; }
        public int device = 1; ///set device to play on
        public float volume = 50f;
        WasapiLoopbackCapture capture;
        WaveFileWriter writer;



        public void startRecording(int num)
        {
            Console.WriteLine("Recording to "+num+".wav");
            capture = new WasapiLoopbackCapture();
            try
            {
                writer = new WaveFileWriter(num + ".wav", capture.WaveFormat);
            }
            catch (IOException){
                stopRecording();
                return;
            }
            capture.StartRecording();
            recording = true;

            capture.DataAvailable += (s, a) =>
            {
                writer.Write(a.Buffer, 0, a.BytesRecorded);
                if (writer.Position > capture.WaveFormat.AverageBytesPerSecond * 20)
                {
                    capture.StopRecording();
                    Console.WriteLine(" Recording is over 20 seconds");
                    if(capture != null){
                        writer.Dispose();
                        writer = null;
                        capture.Dispose();
                        capture = null;
                    }       
                    recording = false;
                }
            };

            capture.RecordingStopped += (s, a) =>
            {
                if(capture != null)
                {
                    writer.Dispose();
                    writer = null;
                    capture.Dispose();
                    capture = null;
                }
                recording = false;
            };
        }
        public void stopRecording()
        {
            Console.WriteLine(" Stopping recording.");
            Form1.window.radioButton1.Checked = Form1.recordMode = false;              
            capture.StopRecording();
            recording = false;
        }



        public void startPlaying(int num)
        {
            if(!File.Exists(Application.StartupPath+'\\'+num + ".wav"))return;

            Console.WriteLine(" Begin playback of " + num + ".wav");
            
            var audioFile = new AudioFileReader(num + ".wav");

            if(device != -1){
                var audioFile2 = new AudioFileReader(num + ".wav");
                var player2 = new WaveOutEvent();
                player2.DeviceNumber = -1;
                player2.Volume = volume/100;
                player2.Init(audioFile2);            

                // begin playback
                player2.Play();

                player2.PlaybackStopped += (s, a) => {
                    if (audioFile != null) audioFile.Dispose();
                    audioFile = null;
                    player2.Dispose();
                };
            }
            var player = new WaveOutEvent();
            player.DeviceNumber = device;
            player.Volume = volume/100;
            player.Init(audioFile);            

            // begin playback
            player.Play();

            player.PlaybackStopped += (s, a) => {
                Console.WriteLine(" Playback ended");
                if (audioFile != null) audioFile.Dispose();
                audioFile = null;
                player.Dispose();
            };
            
        }




    }
}
