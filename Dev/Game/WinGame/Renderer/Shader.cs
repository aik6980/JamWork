using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;

namespace Renderer
{
    public class CachedConstantBuffer<T>  where T : struct
    {
        public T                    m_Cb_CPUBuffer;
        T                           m_Cb_RefBuffer;
        SharpDX.Direct3D11.Buffer   m_Cb_GPUBuffer;

        int                         m_Slot = 0;

        public CachedConstantBuffer(int slot)
        {
            m_Slot = slot;
        }

        public void Init()
        {
            var render_device = Renderer.RenderDevice.Instance();

            m_Cb_GPUBuffer = render_device.CreateBuffer<T>();
            render_device.Device.ImmediateContext.UpdateSubresource(ref m_Cb_RefBuffer, m_Cb_GPUBuffer); 
        }

        public void Destroy()
        {
            m_Cb_GPUBuffer.Dispose();
        }

        public void UpdateData()
        {
            if(!m_Cb_RefBuffer.Equals(m_Cb_CPUBuffer))
            {
                m_Cb_RefBuffer = m_Cb_CPUBuffer;
                var render_device = Renderer.RenderDevice.Instance();
                render_device.Device.ImmediateContext.UpdateSubresource(ref m_Cb_RefBuffer, m_Cb_GPUBuffer); 
            }
        }

        public void Apply()
        {
            Renderer.RenderPipeline.Instance().SetConstantBuffer(m_Slot, m_Cb_GPUBuffer);
        }
    };

    class ShaderGlobal
    {
        static ShaderGlobal m_sSingleton = null; 
        public static ShaderGlobal Instance()
        {
            if(m_sSingleton == null)
            {
                m_sSingleton = new ShaderGlobal();
            }

            return m_sSingleton;
        }

        public struct Cb0
        {
            public Vector4 MousePosition;
            public Vector4 RenderTargetSize;
        };
        public CachedConstantBuffer<Cb0> m_Cb0 = new CachedConstantBuffer<Cb0>(0);

        public void Init()
        {
            m_Cb0.Init();

            var backbuffer_desc = Renderer.RenderDevice.Instance().BackBufferDesc();
            float rt_width  = backbuffer_desc.Width;
            float rt_height = backbuffer_desc.Height;

            m_Cb0.m_Cb_CPUBuffer.RenderTargetSize = new Vector4(rt_width, rt_height, 1/rt_width, 1/rt_height);
            m_Cb0.UpdateData();
        }

        public void Update()
        {
            var vp = Renderer.RenderPipeline.Instance().GetViewport();
            float rt_width  = vp.Width;
            float rt_height = vp.Height;

            var mousePos = Input.InputManager.Instance().ClientMousePosition();

            m_Cb0.m_Cb_CPUBuffer.MousePosition = new Vector4(mousePos.X, mousePos.Y, 0, 0);
            m_Cb0.UpdateData();
        }

        public void Destroy()
        {
            m_Cb0.Destroy();
        }
    };
}