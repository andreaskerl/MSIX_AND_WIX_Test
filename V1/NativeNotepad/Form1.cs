using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace NativeNotepad
{
    public enum ColorSchema
    {
        Default,
        Dark,
        Light,
        Blue
    }

    public partial class Form1 : Form
    {
        public Form1(string path)
        {
            InitializeComponent();

            this.Icon = Properties.Resources.App;
            this.SetColors(); 

            if (File.Exists(path))
                this.LoadFile(path);

        }

        private void SetColors()
        {
            {
                ColorSchema colorSchema = (ColorSchema)Storage.LoadEnum(Microsoft.Win32.Registry.LocalMachine, @"Software\AK\NativeNotepad", "ColorSchema", "Default", typeof(ColorSchema));

                switch (colorSchema)
                {
                    case ColorSchema.Blue:
                        this.textBox1.BackColor = Color.LightSteelBlue;
                        this.textBox1.ForeColor = Color.DarkBlue;
                        break;


                    case ColorSchema.Dark:
                        this.textBox1.BackColor = Color.DimGray;
                        this.textBox1.ForeColor = Color.White;
                        break;

                    case ColorSchema.Light:
                        this.textBox1.BackColor = Color.WhiteSmoke;
                        this.textBox1.ForeColor = Color.Black;
                        break;

                    default:
                        this.textBox1.BackColor = SystemColors.Window;
                        this.textBox1.ForeColor = SystemColors.WindowText;
                        break;
                }
            }
        }


        private void OnMenuItemFileDropDownOpened(object sender, EventArgs e)
        {
            this.menuItemSave.Enabled = (this.textBox1.Lines.Length > 0);
        }


        private void OnMenuItemNewClick(object sender, EventArgs e)
        {
            this.textBox1.Clear();
        }


        private void OnMenuItemOpenClick(object sender, EventArgs e)
        {
            using (OpenFileDialog dlgOpen = new OpenFileDialog())
            {
                dlgOpen.CheckFileExists = true;
                dlgOpen.CheckPathExists = true;
                dlgOpen.Filter = "Textdateien (*.txt)|*.txt|Alle Dateien (*.*)|*.*";

                if (dlgOpen.ShowDialog() == DialogResult.OK)
                    this.LoadFile(dlgOpen.FileName);

            }
        }


        private void OnMenuItemSaveAsClick(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog dlgSave = new SaveFileDialog())
                {
                    dlgSave.Filter = "Textdateien (*.txt)|*.txt|Alle Dateien (*.*)|*.*";

                    if (dlgSave.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllText(dlgSave.FileName, this.textBox1.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void OnMenuItemExitClick(object sender, EventArgs e)
        {
            this.Close();
        }


        private void OnMenuItemInfoClick(object sender, EventArgs e)
        {
            NativeMethods.ShowAbout(this);
        }

        private void LoadFile(string path)
        {
            try
            {
                if (!File.Exists(path))
                    throw new FileNotFoundException();


                this.textBox1.Text = File.ReadAllText(path);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
