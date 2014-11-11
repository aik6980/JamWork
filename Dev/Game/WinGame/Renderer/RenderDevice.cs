using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;

namespace Renderer
{
    class RenderDevice
    {
        static RenderDevice m_sSingleton = null; 
        public static RenderDevice Instance()
        {
            if(m_sSingleton == null)
            {
                m_sSingleton = new RenderDevice();
            }

            return m_sSingleton;
        }

        SharpDX.Direct3D11.Device m_Device = null;
        public SharpDX.Direct3D11.Device Device
        {
            get { return m_Device; }
        }
        SwapChain                   m_SwapChain = null;

        RenderTargetView            m_RTView;
        public SharpDX.Direct3D11.RenderTargetView RTView
        {
            get { return m_RTView; }
        }

        public Texture2DDescription BackBufferDesc()
        {
            var backBuffer = Texture2D.FromSwapChain<Texture2D>(m_SwapChain, 0);
            return backBuffer.Description;
        }

        public void Init()
        {
            var form = WinGame.Program.RenderForm;
            // swap chain desc
            var desc = new SwapChainDescription()
                        {
                               BufferCount = 2,
                               ModeDescription = 
                                   new ModeDescription(form.ClientSize.Width, form.ClientSize.Height,
                                                       new Rational(60, 1), Format.B8G8R8A8_UNorm),
                               IsWindowed = true,
                               OutputHandle = form.Handle,
                               SampleDescription = new SampleDescription(1, 0),
                               SwapEffect = SwapEffect.Discard,
                               Usage = Usage.RenderTargetOutput
                        };

            var creationFlags = DeviceCreationFlags.Debug;

            Factory factory = new Factory();
            var adapter = factory.GetAdapter(0);
            m_Device = new SharpDX.Direct3D11.Device(adapter, creationFlags);
            m_SwapChain = new SwapChain(factory, m_Device, desc);

            //SharpDX.Direct3D11.Device.CreateWithSwapChain(DriverType.Hardware, creationFlags, desc, 
            //   out m_Device, out m_SwapChain);
 
            // Ignore all windows events
            //var factory = m_SwapChain.GetParent<Factory>();
            factory.MakeWindowAssociation(form.Handle, WindowAssociationFlags.IgnoreAll);

            // New RenderTargetView from the backbuffer
            var backBuffer = Texture2D.FromSwapChain<Texture2D>(m_SwapChain, 0);
            m_RTView= new RenderTargetView(m_Device, backBuffer);
        }

        public void Present()
        {
            m_SwapChain.Present(0, PresentFlags.None);
        }
    }
}