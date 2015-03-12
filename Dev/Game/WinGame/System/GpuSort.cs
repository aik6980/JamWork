using System.Runtime.InteropServices;

using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;

using Renderer;

using D3DBuffer = SharpDX.Direct3D11.Buffer;

namespace System
{
    class GpuSort
    {
        // Buffer
        D3DBuffer               m_DataBuffer = null;
        UnorderedAccessView     m_UAView     = null;

        int                     m_NumItems   = 16;

        public void Init()
        {
            var dev = Renderer.RenderDevice.Instance().Device;

            int structSize  = Utilities.SizeOf<Point>();
            int sizeInBytes = m_NumItems * structSize;

            m_DataBuffer = new D3DBuffer(dev, new BufferDescription()
            {
                BindFlags = BindFlags.UnorderedAccess,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.BufferStructured,
                SizeInBytes = sizeInBytes,
                StructureByteStride = structSize,
                Usage = ResourceUsage.Default,
            });

            m_UAView = new UnorderedAccessView(dev, m_DataBuffer);

            // upload data
            Point[] data = new Point[m_NumItems];
            for( int i=0; i<data.Length; ++i)
            {
                data[i] = new Point( -1, 0 );
            }

            dev.ImmediateContext.UpdateSubresourceSafe(data, m_DataBuffer, structSize);
        }

        public void Destroy()
        {

        }

        public void Update()
        {

        }
    };
};