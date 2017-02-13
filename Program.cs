using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace SubComponentExporter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        // ja - Mutex can be made static so that GC doesn't recycle same effect with GC.KeepAlive(mutex) at the end of main
        static Mutex mutex = new Mutex(false, "SubComponentExporter");

        [STAThread]
        static void Main()
        {
            // ja - if you like to wait a few seconds in case that the instance is just shutting down
            if (!mutex.WaitOne(TimeSpan.FromSeconds(1), false))
            {
                MessageBox.Show("Sub Component Exporter is already running!", "", MessageBoxButtons.OK);
                return;
            }

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new SubCompExport());
            }
            finally // ja - release Mutex
            { 
                mutex.ReleaseMutex(); 
            } 
        }
    }
}
