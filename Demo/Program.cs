using Demo.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using XCore;
using System.Diagnostics;

namespace Demo
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            //Check process
            var exists = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Count() > 1;

            //If process is exist
            if (exists)
            {
                MessageBox.Show("The application has already run!");
                Application.Exit();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(Form1.Instance);

            //var name = Process.GetCurrentProcess().ProcessName;
            //var names = Process.GetProcessesByName(name);
            //if (names.Count() > 1)
            //{
            //    BzMessagebox.Show("Application is runing in Background Process！\rUsing TaskManager to find and shutdown it...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //try
            //{
            //    Application.Run(new Form1());
            //}
            //catch (Exception ex)
            //{
            //    BzMessagebox.Show("Exception is :" + "\n\n" + ex.StackTrace + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            //}
        }
    }
}
