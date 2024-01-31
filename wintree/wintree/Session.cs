using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace wintree
{
    class Session
    {
        private string name;
        private string command;
        private string parameters;


        public Session(string name, string command, string parameters)
        {
            this.name = name;
            this.command = command;
            this.parameters = parameters;

            this.Start();

        }


        public void Start()
        {

            try
            {

                var process = new Process
                {
                    StartInfo = new ProcessStartInfo(command, parameters) { UseShellExecute = false, RedirectStandardOutput = true }
                };

                process.Start();
                WriteLog("[" + DateTime.Now + "] Start " + name + " (" + command + ")");

                var outputResultPromise = process.StandardOutput.ReadToEndAsync();
                outputResultPromise.ContinueWith(o => WriteLog("[" + DateTime.Now + "] End " + name + " (" + command + ")"));

            }

            catch (Exception e)
            {
                MessageBox.Show("Something went wrong: \n" + e.Message);


            }


        }


        private int WriteLog(string s)
        {

            try
            {
                string filename = Settings.Default.LogFile;
                File.AppendAllText(filename, s + Environment.NewLine);

            }

            catch (Exception e)
            {
                MessageBox.Show("Something went wrong: \n" + e.Message);
                return -1;

            }

            return 0;



        }

    }
}
