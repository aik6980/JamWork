using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// SharpDX
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;

using SharpDX.Toolkit;

using Renderer;

namespace WinGame
{
    class GameApp : Game
    {
        
    };

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var app = new GameApp();
            app.Run();
        }
    }
}
