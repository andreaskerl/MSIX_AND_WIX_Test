using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NativeNotepad
{
    public static class NativeMethods
    {
        [DllImport("shell32.dll")]
        private static extern int ShellAbout(IntPtr hWnd, string szApp, string szOtherStuff, IntPtr hIcon);



        /// <summary>
        /// Aboutdialog anzeigen
        /// </summary>
        /// <param name="owner">Referenz auf die Hauptform</param>
        public static void ShowAbout(System.Windows.Forms.Form owner)
        {
                // Deklarationen
                string description = string.Empty;
                string title = string.Empty;


                // Assembly bestimmen und Attribute abfragen
                System.Reflection.Assembly Asm = System.Reflection.Assembly.GetEntryAssembly();
                System.Reflection.AssemblyDescriptionAttribute assemblyDescription = (System.Reflection.AssemblyDescriptionAttribute)System.Reflection.AssemblyDescriptionAttribute.GetCustomAttribute(Asm, typeof(System.Reflection.AssemblyDescriptionAttribute));
                System.Reflection.AssemblyTitleAttribute assemblyTitle = (System.Reflection.AssemblyTitleAttribute)System.Reflection.AssemblyTitleAttribute.GetCustomAttribute(Asm, typeof(System.Reflection.AssemblyTitleAttribute));

                if (assemblyTitle != null)
                    title = assemblyTitle.Title;


            // About aufrufen
            NativeMethods.ShellAbout(owner.Handle, string.Format("{0} ({1})", title, Asm.GetName().Version.ToString()), string.Empty, owner.Icon.Handle);
            //NativeMethods.ShellAbout(owner.Handle, string.Format("{0} ({1})", title, Asm.GetName().Version.ToString()), databaseConnectionInfo + Environment.NewLine + storageSetting, owner.Icon.Handle);

        }
    }
}
