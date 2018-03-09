using System;
using System.Collections.Generic;
using System.Text;

namespace Grumpydev.Net.Essentials.Core
{
    public class LocalFileSystem : IFileSystem
    {
        public string GetAllText(string fileName)
        {
            return System.IO.File.ReadAllText(fileName);
        }

        public string SetDirectory(string path)
        {
            throw new NotImplementedException();
        }
    }
}
