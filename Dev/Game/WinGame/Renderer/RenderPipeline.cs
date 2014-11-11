using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;

namespace Renderer
{
    class RenderPipeline
    {
        static RenderPipeline m_sSingleton = null; 
        public static RenderPipeline Instance()
        {
            if(m_sSingleton == null)
            {
                m_sSingleton = new RenderPipeline();
            }

            return m_sSingleton;
        }

        // RenderTarget
        Viewport            m_Viewport;
        RenderTargetView    m_RenderTarget          = null;
        DepthStencilView    m_DepthStencilView      = null;

        // Shader Pipeline
        VertexBufferBinding m_VertexBufferBinding   = new VertexBufferBinding();
        VertexShader        m_VertexShader          = null;
        PixelShader         m_PixelShader           = null;

        public void Init()
        {
            var context = Renderer.RenderDevice.Instance().Device.ImmediateContext;

            m_RenderTarget = Renderer.RenderDevice.Instance().RTView;
            var backbuffer_desc = Renderer.RenderDevice.Instance().BackBufferDesc();
            m_Viewport = new Viewport(0, 0, backbuffer_desc.Width, backbuffer_desc.Height);

            // Prepare All the stages, set to default
            context.InputAssembler.InputLayout = null;
            context.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;

            context.InputAssembler.SetVertexBuffers(0, m_VertexBufferBinding);
            context.VertexShader.SetConstantBuffer(0, null);
            context.VertexShader.Set(m_VertexShader);
            context.Rasterizer.SetViewport(m_Viewport);
            context.PixelShader.Set(m_PixelShader);
            //context.PixelShader.SetSampler(0, sampler);
            //context.PixelShader.SetShaderResource(0, textureView);
            context.OutputMerger.SetTargets(m_DepthStencilView, m_RenderTarget);

            //context.ClearRenderTargetView(m_RenderTarget, Color.Cyan);
        }

        public void SetRenderTarget(RenderTargetView input)
        {
            if(m_RenderTarget != input)
            {
                m_RenderTarget = input;
                var context = Renderer.RenderDevice.Instance().Device.ImmediateContext;
                context.OutputMerger.SetTargets(m_RenderTarget);
            }
        }

        public void SetViewport(Viewport input)
        {
            if(m_Viewport != input)
            {
                m_Viewport = input;
                var context = Renderer.RenderDevice.Instance().Device.ImmediateContext;
                context.Rasterizer.SetViewport(m_Viewport);
            }
        }

        public void SetVertexShader(VertexShader input)
        {
            if (m_VertexShader != input)
            {
                m_VertexShader = input;
                var context = Renderer.RenderDevice.Instance().Device.ImmediateContext;
                context.VertexShader.Set(m_VertexShader);
            }
        }

        public void SetPixelShader(PixelShader input)
        {
            if (m_PixelShader != input)
            {
                m_PixelShader = input;
                var context = Renderer.RenderDevice.Instance().Device.ImmediateContext;
                context.PixelShader.Set(m_PixelShader);
            }
        }
    };
};