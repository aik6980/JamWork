using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;

using Renderer;

namespace Graphic
{
    class FinalCompositionShader
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
            var shader = Graphic.ShaderManager.Instance().Find(RenderShaderEnum.FINAL_COMPOSITION);
            Renderer.RenderPipeline.Instance().SetVertexShader(shader.m_VS);
            Renderer.RenderPipeline.Instance().SetPixelShader(shader.m_PS);
        }
    }

    class FinalCompositionView : ISceneView
    {
        FinalCompositionShader  m_Shader = null;

        // Buffer
        RenderTargetView    m_RTView = null;

        Viewport            m_Viewport;

        public void Init()
        {
            m_Shader = new FinalCompositionShader();
            m_Shader.Init();

            // create RT
            var dev_context = Renderer.RenderDevice.Instance().Device;

            m_RTView = Renderer.RenderDevice.Instance().RTView;

            var backbuffer_desc = Renderer.RenderDevice.Instance().BackBufferDesc();
            var rt_width = backbuffer_desc.Width;
            var rt_height = backbuffer_desc.Height;
            m_Viewport = new Viewport(0,0,rt_width,rt_height);
        }

        public void Destroy()
        {
            m_Shader.Destroy();
        }

        public void Update()
        {

        }

        public void Render()
        {
            var context = Renderer.RenderDevice.Instance().Device.ImmediateContext;

            Renderer.RenderPipeline.Instance().SetRenderTarget(m_RTView);
            Renderer.RenderPipeline.Instance().SetViewport(m_Viewport);

            var backbuffer_desc = Renderer.RenderDevice.Instance().BackBufferDesc();
            float rt_width = backbuffer_desc.Width;
            float rt_height = backbuffer_desc.Height;

            ShaderGlobal.Instance().m_Cb0.m_Cb_CPUBuffer.RenderTargetSize = new Vector4( rt_width, rt_height, 1.0f/rt_width, 1.0f/rt_height );
            ShaderGlobal.Instance().m_Cb0.UpdateData();
            ShaderGlobal.Instance().m_Cb0.Apply();

            var envView = Graphic.SceneViewManager.Instance().GetView<EnvironmentView>(SceneViewType.ENVIRONMENT_VIEW);
            if(envView != null)
            {
                RenderPipeline.Instance().SetShaderResourceViewPS(0, envView.RenderTargetSRV() );
                RenderPipeline.Instance().SetSamplerStatePS(0, RenderStateGlobal.Instance().BilinearClampSampler );
            }


            m_Shader.Apply();
            context.Draw(3,0);

            // clear states
            RenderPipeline.Instance().SetShaderResourceViewPS(0, null );
        }
    };
};