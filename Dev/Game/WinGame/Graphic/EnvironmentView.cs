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
        public void Init()
        {

        }

        public void Destroy()
        {

        }

        public void Apply()
        {
            // bind shader
            var shader = Graphic.ShaderManager.Instance().Find(RenderShaderEnum.ENVIRONMENT);
            Renderer.RenderPipeline.Instance().SetVertexShader(shader.m_VS);
            Renderer.RenderPipeline.Instance().SetPixelShader(shader.m_PS);

            // bind constants
            Renderer.ShaderGlobal.Instance().m_Cb0.Apply();
        }
    }

    class EnvironmentView : ISceneView
    {
        EnvironmentShader   m_Shader = null;

        // Buffer
        Texture2D           m_RenderTarget = null;
        RenderTargetView    m_RTView = null;
        ShaderResourceView  m_SRView = null;

        Viewport            m_Viewport;

        public ShaderResourceView RenderTargetSRV() { return m_SRView; }

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
            m_SRView = new ShaderResourceView( dev_context, m_RenderTarget );

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

            Renderer.RenderPipeline.Instance().SetRenderTarget(m_RTView);
            Renderer.RenderPipeline.Instance().SetViewport(m_Viewport);

            float rt_width = m_Viewport.Width;
            float rt_height = m_Viewport.Height;

            ShaderGlobal.Instance().m_Cb0.m_Cb_CPUBuffer.RenderTargetSize = new Vector4(rt_width, rt_height, 1.0f / rt_width, 1.0f / rt_height);
            ShaderGlobal.Instance().m_Cb0.UpdateData();

            m_Shader.Apply();
            context.Draw(3,0);
        }
    };
};