using System;
using System.Collections.Generic;
using System.Text;

namespace Grumpydev.Net.Essentials.Core
{
    public interface IFileSystem
    {
        string SetDirectory(string path);

        string GetAllText(string fileName);
    }
}
