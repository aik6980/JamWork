using System.Runtime.InteropServices;

using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;

using Renderer;

namespace System
{
    public struct Pixel3232_RG
    {
        float R;
        float G;

        public Pixel3232_RG(float r, float g)
        {
            R = r; G = g;
        }
    }

    class GpuSort : Util.Singleton<GpuSort>
    {
        // Buffer
        Texture2D               m_DataTexture = null;
        UnorderedAccessView     m_UAView;

        public void Init()
        {
            var dev = Renderer.RenderDevice.Instance().Device;

            m_DataTexture = new Texture2D(dev, new Texture2DDescription()
            {
                Format = Format.R32G32_Float,
                Width = 4,
                Height = 4,
                ArraySize = 1,
                BindFlags = BindFlags.UnorderedAccess,
                CpuAccessFlags = CpuAccessFlags.None,
                MipLevels = 1,
                OptionFlags = ResourceOptionFlags.None,
                SampleDescription = new SampleDescription(1, 0),
                Usage = ResourceUsage.Default,
            });

            m_UAView = new UnorderedAccessView(dev, m_DataTexture);

            // upload data
            Pixel3232_RG[] data = new Pixel3232_RG[4*4];
            for( int i=0; i<data.Length; ++i)
            {
                data[i] = new Pixel3232_RG( 0.25f, 0.0f );
            }

            dev.ImmediateContext.UpdateSubresourceSafe(data, m_DataTexture, 16);
        }

        public void Destroy()
        {

        }

        public void Update()
        {

        }
    };
};