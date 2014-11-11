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
    };
};