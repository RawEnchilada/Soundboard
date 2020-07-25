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
        public float volume = 1.0f;
        public WaveOutEvent player;
        WasapiLoopbackCapture capture;
        WaveFileWriter writer;

        

        public void startRecording(int num)
        {
            Console.WriteLine("Recording to "+num+".wav");
            capture = new WasapiLoopbackCapture();
            writer = new WaveFileWriter(num + ".wav", capture.WaveFormat);
            capture.StartRecording();
            recording = true;
            capture.DataAvailable += (s, a) =>
            {
                writer.Write(a.Buffer, 0, a.BytesRecorded);
                if (writer.Position > capture.WaveFormat.AverageBytesPerSecond * 20)
                {
                    capture.StopRecording();
                    Console.WriteLine(" Recording is over 20 seconds");
                    recording = false;
                }
            };

            capture.RecordingStopped += (s, a) =>
            {
                writer.Dispose();
                writer = null;
                capture.Dispose();
                capture = null;
                recording = false;
            };
        }
        public void stopRecording()
        {
            Console.WriteLine(" Stopping recording.");
            capture.StopRecording();
        }



        public void startPlaying(int num)
        {
            if(!File.Exists(Application.StartupPath+'\\'+num + ".wav"))return;

            if(player.PlaybackState == PlaybackState.Playing)
            {
                player.Stop();
            }

            Console.WriteLine(" Begin playback of " + num + ".wav");

            // set up playback
            var audioFile = new AudioFileReader(num + ".wav");
            player = new WaveOutEvent();
            player.DeviceNumber = device;
            player.Volume = volume/100;
            player.Init(audioFile);            

            // begin playback
            player.Play();

            player.PlaybackStopped += (s, a) => {
                Console.WriteLine(" Playback ended");
                if (player != null) player.Dispose();
                player = null;
                if (audioFile != null) audioFile.Dispose();
                audioFile = null;
            };
            
        }




    }
}
