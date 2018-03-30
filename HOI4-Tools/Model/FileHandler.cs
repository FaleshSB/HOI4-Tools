using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOI4_Tools.Model
{
    static class FileHandler
    {
        private static string filteredLocation;

        static FileHandler()
        {
            string baseLocation = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            filteredLocation = System.IO.Path.GetDirectoryName(baseLocation).Replace("file:\\", "") + "\\Data\\";
            if (!Directory.Exists(filteredLocation)) Directory.CreateDirectory(filteredLocation);
        }

        public static void SaveFile(string fileName, string data)
        {
            File.WriteAllText(Path.Combine(filteredLocation, fileName), data);
        }
        public static string[] LoadFile(string fileName)
        {
            if (File.Exists(Path.Combine(filteredLocation, fileName)))
            {
                return File.ReadAllLines(Path.Combine(filteredLocation, fileName));
            }
            else
            {
                return null;
            }
        }
    }
}