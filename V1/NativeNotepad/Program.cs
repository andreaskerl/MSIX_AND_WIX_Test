using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NativeNotepad
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string file = string.Empty;
            if (args.Length > 0)
                file = args[0];
            else
                file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), @"NativeNotepad\Help.txt");

            Application.Run(new Form1(file));

        }
    }
}
