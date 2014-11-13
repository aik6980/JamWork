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

        SharpDX.Direct3D11.Buffer[]             m_ConstantBuffer        = new SharpDX.Direct3D11.Buffer[16];
        SharpDX.Direct3D11.ShaderResourceView[] m_ShaderResourceView    = new SharpDX.Direct3D11.ShaderResourceView[16];
        SharpDX.Direct3D11.SamplerState[]       m_SamplerState          = new SharpDX.Direct3D11.SamplerState[16];

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

        public void Destroy()
        {

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

        public Viewport GetViewport() { return m_Viewport; }
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

        public void SetConstantBuffer(int slot, SharpDX.Direct3D11.Buffer input)
        {
            if(m_ConstantBuffer[slot] != input)
            {
                m_ConstantBuffer[slot] = input;
                var context = Renderer.RenderDevice.Instance().Device.ImmediateContext;
                // set constant buffer to every stages
                context.VertexShader.SetConstantBuffer(slot, m_ConstantBuffer[slot]);
                context.PixelShader.SetConstantBuffer(slot, m_ConstantBuffer[slot]);
            }
        }

        public void SetShaderResourceViewPS(int slot, ShaderResourceView input)
        {
            if(m_ShaderResourceView[slot] != input)
            {
                m_ShaderResourceView[slot] = input;
                var context = Renderer.RenderDevice.Instance().Device.ImmediateContext;
                // set constant buffer to every stages
                context.PixelShader.SetShaderResource(slot, m_ShaderResourceView[slot]);
            }
        }

        public void SetSamplerStatePS(int slot, SamplerState input)
        {
            if (m_SamplerState[slot] != input)
            {
                m_SamplerState[slot] = input;
                var context = Renderer.RenderDevice.Instance().Device.ImmediateContext;
                // set constant buffer to every stages
                context.PixelShader.SetSampler(slot, m_SamplerState[slot]);
            }
        }
    };
};