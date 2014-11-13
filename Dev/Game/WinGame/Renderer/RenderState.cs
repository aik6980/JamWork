using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;

namespace Renderer
{
    class RenderStateGlobal : Util.Singleton<RenderStateGlobal>
    {
        SamplerState m_BilinearClampSampler;
        public SamplerState BilinearClampSampler
        {
            get { return m_BilinearClampSampler; }
        }


        public void Init()
        {
            var device = Renderer.RenderDevice.Instance().Device;

            {
                SamplerStateDescription description = SamplerStateDescription.Default();
                description.Filter = Filter.MinMagMipLinear;
                description.AddressU = TextureAddressMode.Wrap;
                description.AddressV = TextureAddressMode.Wrap;
                m_BilinearClampSampler = new SamplerState(device, description);
            }
        }

        public void Destroy()
        {
            m_BilinearClampSampler.Dispose();
        }
    };
}