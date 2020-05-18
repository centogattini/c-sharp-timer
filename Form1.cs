using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Time
{
    public partial class Form1 : Form
    {
        static DateTime time;
        private static Mutex mut = new Mutex();
        public Form1()
        {
            InitializeComponent();

            time = new DateTime();

            Thread newThreadS = new Thread(new ThreadStart(ThreadProc));
            newThreadS.Name = String.Format("Seconds");

            Thread newThreadM = new Thread(new ThreadStart(ThreadProc));
            newThreadM.Name = String.Format("Minutes");

            Thread newThreadH = new Thread(new ThreadStart(ThreadProc));
            newThreadH.Name = String.Format("Hours");

            newThreadH.Start();
            newThreadS.Start();
            newThreadM.Start();
        }
        private void ThreadProc()
        {
            int n = 10; // Здесь можно выбрать время в течение которого будет работать программа (в секундах)
            for (int i = 0; i < n; i++)
            {
                mut.WaitOne();

                time = DateTime.Now;
                char[] prevTime = label1.Text.ToCharArray();

                if (Thread.CurrentThread.Name == "Hours")
                {
                    string h = time.Hour.ToString();
                    prevTime[0] = h[0];
                    prevTime[1] = h[1];
                }
                if (Thread.CurrentThread.Name == "Minutes")
                {
                    string m = time.Minute.ToString();
                    prevTime[3] = m[0];
                    prevTime[4] = m[1];
                }
                if (Thread.CurrentThread.Name == "Seconds")
                {
                    string s = time.Second.ToString();
                    prevTime[6] = s[0];
                    prevTime[7] = s[1];
                }    
                mut.ReleaseMutex();
                label1.Text = new string(prevTime);
                Thread.Sleep(1000);
            }
        }
        private void charArrayToString()
        {
            
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

    }
}
