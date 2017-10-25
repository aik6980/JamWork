using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinGame.Contents
{
    class MeshLoader
    {
        public void LoadFromFile(string fn)
        {
            // using StreamReader is quicker, however, this is pretty convenience
            string[] content = File.ReadAllLines(fn);

            foreach(string line in content)
            {

            }
        }
    }
}
