using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NativeNotepad
{


    public static class Storage
    {
        public static string LoadString(RegistryKey Root, string Key, string Name, string DefaultValue)
        {
            // Rückgabewert
            string _return = DefaultValue;

            // Registryschlüssel öffnen
            RegistryKey rk = Root.OpenSubKey(Key, false);
            if (rk != null)
            {
                _return = rk.GetValue(Name, DefaultValue).ToString();
                rk.Close();
            }

            // Rückgabe
            return (_return);
        }


        public static int LoadEnum(Microsoft.Win32.RegistryKey Root, string Key, string Name, string DefaultValue, Type enumType)
        {
            string currentValue = Storage.LoadString(Root, Key, Name, DefaultValue);

            if (Enum.IsDefined(enumType, currentValue))
                return ((int)Enum.Parse(enumType, currentValue));
            else
                return ((int)Enum.Parse(enumType, DefaultValue));

        }
    }
}
