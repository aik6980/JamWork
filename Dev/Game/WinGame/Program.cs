using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;

using Renderer;
using Graphic;

namespace WinGame
{
    class Program
    {
        static RenderForm m_sRenderForm = null;
        static public RenderForm RenderForm
        {
            get { return m_sRenderForm; }
        }

        // scene
        static EnvironmentView m_EnvironmentView = new EnvironmentView();

        static void Init()
        {
            // system
            Renderer.RenderDevice.Instance().Init();
            Renderer.RenderPipeline.Instance().Init();

            // scene
            m_EnvironmentView.Init();
        }

        static void Update()
        {
            // Game update

            // Draw update
            m_EnvironmentView.Render();

            // end of the frame
            Renderer.RenderDevice.Instance().Present();
        }

        static void Destroy()
        {

        }

        [STAThread]
        static void Main(string[] args)
        {
            m_sRenderForm = new RenderForm("WinGame");

            Init();
            RenderLoop.Run( m_sRenderForm, Update );
            Destroy();
        }
    };
}
