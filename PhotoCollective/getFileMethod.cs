using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoCollective
{
    public static class getFileMethod
    {
        public static List<string> GetAllAccessibleFiles(string rootPath, List<string> alreadyFound = null)
        {
            if (alreadyFound == null)
                alreadyFound = new List<string>();
            DirectoryInfo di = new DirectoryInfo(rootPath);
            var dirs = di.EnumerateDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                if (!((dir.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden))
                {
                    alreadyFound = GetAllAccessibleFiles(dir.FullName, alreadyFound);
                }
            }

            var files = Directory.GetFiles(rootPath);
            var filters = new String[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp", "svg" };
            foreach (string s in files)
            {
                foreach (var filter in filters)
                {
                    if (s.Contains(filter))
                    {
                        alreadyFound.Add(s);
                    }
                }
            }

            return alreadyFound;
        }
    }
}
