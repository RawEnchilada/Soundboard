using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;
using System.Runtime.InteropServices;

namespace Soundboard_forms
{
    public partial class Form1 : Form
    {
        public static Form1 window;

        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);

        public Form1()
        {
            InitializeComponent();

            numericUpDown1.Minimum = -1;
            numericUpDown1.Maximum = WaveOut.DeviceCount - 1;
            numericUpDown1.Value = -1;

            label3.Text = "" + Program.audio.volume;
            KeyPreview = true;
            

            int UniqueHotkeyId0 = 0;
            int HotKeyCode0 = (int)Keys.NumPad0;
            Boolean Registered0 = RegisterHotKey(
                this.Handle, UniqueHotkeyId0, 0x0000, HotKeyCode0
            );

            int UniqueHotkeyId1 = 1;
            int HotKeyCode1 = (int)Keys.NumPad1;
            Boolean Registered1 = RegisterHotKey(
                this.Handle, UniqueHotkeyId1, 0x0000, HotKeyCode1
            );

            int UniqueHotkeyId2 = 2;
            int HotKeyCode2 = (int)Keys.NumPad2;
            Boolean Registered2 = RegisterHotKey(
                this.Handle, UniqueHotkeyId2, 0x0000, HotKeyCode2
            );

            int UniqueHotkeyId3 = 3;
            int HotKeyCode3 = (int)Keys.NumPad3;
            Boolean Registered3 = RegisterHotKey(
                this.Handle, UniqueHotkeyId3, 0x0000, HotKeyCode3
            );

            int UniqueHotkeyId4 = 4;
            int HotKeyCode4 = (int)Keys.NumPad4;
            Boolean Registered4 = RegisterHotKey(
                this.Handle, UniqueHotkeyId4, 0x0000, HotKeyCode4
            );

            int UniqueHotkeyId5 = 5;
            int HotKeyCode5 = (int)Keys.NumPad5;
            Boolean Registered5 = RegisterHotKey(
                this.Handle, UniqueHotkeyId5, 0x0000, HotKeyCode5
            );

            int UniqueHotkeyId6 = 6;
            int HotKeyCode6 = (int)Keys.NumPad6;
            Boolean Registered6 = RegisterHotKey(
                this.Handle, UniqueHotkeyId6, 0x0000, HotKeyCode6
            );

            int UniqueHotkeyId7 = 7;
            int HotKeyCode7 = (int)Keys.NumPad7;
            Boolean Registered7 = RegisterHotKey(
                this.Handle, UniqueHotkeyId7, 0x0000, HotKeyCode7
            );

            int UniqueHotkeyId8 = 8;
            int HotKeyCode8 = (int)Keys.NumPad8;
            Boolean Registered8 = RegisterHotKey(
                this.Handle, UniqueHotkeyId8, 0x0000, HotKeyCode8
            );

            int UniqueHotkeyId9 = 9;
            int HotKeyCode9 = (int)Keys.NumPad9;
            Boolean Registered9 = RegisterHotKey(
                this.Handle, UniqueHotkeyId9, 0x0000, HotKeyCode9
            );


        }

        public static bool recordMode = false;

        protected override void WndProc(ref Message m)
        {

            if (m.Msg == 0x0312)
            {
                int id = m.WParam.ToInt32();

                

                switch (id)
                {
                    case 0:    
                        if (Program.audio.recording) {
                            Program.audio.stopRecording();
                            break;
                        }                  
                        radioButton1.Checked = recordMode = true;                        
                        break;
                    case 1:
                        if (Program.audio.recording) {
                            Program.audio.stopRecording();
                            break;
                        } 
                        if (recordMode) Program.audio.startRecording(1);
                        else Program.audio.startPlaying(1);
                        break;
                    case 2:
                        if (Program.audio.recording) {
                            Program.audio.stopRecording();
                            break;
                        } 
                        if (recordMode) Program.audio.startRecording(2);
                        else Program.audio.startPlaying(2);
                        break;
                    case 3:
                        if (Program.audio.recording) {
                            Program.audio.stopRecording();
                            break;
                        } 
                        if (recordMode) Program.audio.startRecording(3);
                        else Program.audio.startPlaying(3);
                        break;
                    case 4:
                        if (Program.audio.recording) {
                            Program.audio.stopRecording();
                            break;
                        } 
                        if (recordMode) Program.audio.startRecording(4);
                        else Program.audio.startPlaying(4);
                        break;
                    case 5:
                        if (Program.audio.recording) {
                            Program.audio.stopRecording();
                            break;
                        } 
                        if (recordMode) Program.audio.startRecording(5);
                        else Program.audio.startPlaying(5);
                        break;
                    case 6:
                        if (Program.audio.recording) {
                            Program.audio.stopRecording();
                            break;
                        } 
                        if (recordMode) Program.audio.startRecording(6);
                        else Program.audio.startPlaying(6);
                        break;
                    case 7:
                        if (Program.audio.recording) {
                            Program.audio.stopRecording();
                            break;
                        } 
                        if (recordMode) Program.audio.startRecording(7);
                        else Program.audio.startPlaying(7);
                        break;
                    case 8:
                        if (Program.audio.recording) {
                            Program.audio.stopRecording();
                            break;
                        } 
                        if (recordMode) Program.audio.startRecording(8);
                        else Program.audio.startPlaying(8);
                        break;
                    case 9:
                        if (Program.audio.recording) {
                            Program.audio.stopRecording();
                            break;
                        } 
                        if (recordMode) Program.audio.startRecording(9);
                        else Program.audio.startPlaying(9);
                        break;
                }
            }

            base.WndProc(ref m);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if(Program.audio != null)Program.audio.device = (int)numericUpDown1.Value;

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (Program.audio != null)
            {
                Program.audio.volume = trackBar1.Value;
                label3.Text = "" + Program.audio.volume;
            }
        }

        public static void Label3(string text)
        {
            window.label3.Text = text;
        }
        
    }
}
