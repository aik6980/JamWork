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
using Input;

namespace WinGame
{
    class Program
    {
        static RenderForm m_sRenderForm = null;
        static public RenderForm RenderForm
        {
            get { return m_sRenderForm; }
        }

        static void Init()
        {
            // system
            Renderer.RenderDevice.Instance().Init();
            Renderer.RenderPipeline.Instance().Init();
            Renderer.ShaderGlobal.Instance().Init();
            Renderer.RenderStateGlobal.Instance().Init();
            Input.InputManager.Instance().Init();

            // scene
            Graphic.SceneViewManager.Instance().Init();
        }

        static void Update()
        {
            // Game update
            Input.InputManager.Instance().Update();
            Renderer.ShaderGlobal.Instance().Update();

            // Draw update
            Graphic.SceneViewManager.Instance().Render();

            // end of the frame
            Renderer.RenderDevice.Instance().Present();
        }

        static void Destroy()
        {
            Graphic.SceneViewManager.Instance().Destroy();

            Input.InputManager.Instance().Destroy();
            Renderer.RenderStateGlobal.Instance().Destroy();
            Renderer.ShaderGlobal.Instance().Destroy();
            Renderer.RenderPipeline.Instance().Destroy();
            Renderer.RenderDevice.Instance().Destroy();
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
