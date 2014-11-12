using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;

using Renderer;

namespace Graphic
{
    class EnvironmentShader
    {
        VertexShader    m_VS = null;
        PixelShader     m_PS = null;

        public void Init()
        {
            var dev_context = Renderer.RenderDevice.Instance().Device;

            var vs_bytecode = ShaderBytecode.FromFile( "Shaders/fullscreen_tri_vs.cso" );
            m_VS = new VertexShader( dev_context, vs_bytecode );

            var ps_bytecode = ShaderBytecode.FromFile( "Shaders/final_composition_ps.cso" );
            m_PS = new PixelShader( dev_context, ps_bytecode );
        }

        public void Destroy()
        {
            m_PS.Dispose();
            m_VS.Dispose();
        }

        public void Apply()
        {
            // bind shader
            Renderer.RenderPipeline.Instance().SetVertexShader(m_VS);
            Renderer.RenderPipeline.Instance().SetPixelShader(m_PS);

            // bind constants
            Renderer.ShaderGlobal.Instance().m_Cb0.Apply();
        }
    }

    class EnvironmentView
    {
        EnvironmentShader   m_Shader = null;

        // Buffer
        Texture2D           m_RenderTarget = null;
        RenderTargetView    m_RTView = null;

        Viewport            m_Viewport;

        public void Init()
        {
            m_Shader = new EnvironmentShader();
            m_Shader.Init();

            // create RT
            var dev_context = Renderer.RenderDevice.Instance().Device;
            var backbuffer_desc = Renderer.RenderDevice.Instance().BackBufferDesc();
            var scale = 2;

            var rt_width = backbuffer_desc.Width * scale;
            var rt_height = backbuffer_desc.Height * scale;
            m_RenderTarget = new Texture2D(dev_context, new Texture2DDescription()
            {
                Format = Format.R8G8B8A8_UNorm,
                Width = rt_width,
                Height = rt_height,
                ArraySize = 1,
                BindFlags = BindFlags.RenderTarget | BindFlags.ShaderResource,
                CpuAccessFlags = CpuAccessFlags.None,
                MipLevels = 1,
                OptionFlags = ResourceOptionFlags.None,
                SampleDescription = new SampleDescription(1, 0),
                Usage = ResourceUsage.Default,
            });

            m_RTView = new RenderTargetView( dev_context, m_RenderTarget );

            m_Viewport = new Viewport(0,0,rt_width,rt_height);
        }

        public void Destroy()
        {
            m_Shader.Destroy();
            m_RenderTarget.Dispose();
        }

        public void Update()
        {

        }

        public void Render()
        {
            var context = Renderer.RenderDevice.Instance().Device.ImmediateContext;

            //Renderer.RenderPipeline.Instance().SetRenderTarget(m_RTView);
            //Renderer.RenderPipeline.Instance().SetViewport(m_Viewport);

            m_Shader.Apply();
            context.Draw(3,0);
        }
    };
};