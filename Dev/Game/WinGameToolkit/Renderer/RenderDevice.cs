using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;

using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;

namespace Renderer
{
    class RenderDevice
    {
        static RenderDevice m_sRenderDevice = null;
        static RenderDevice GlobalObj()
        {
            if(m_sRenderDevice == null)
            {
                m_sRenderDevice = new RenderDevice();
            }
            return m_sRenderDevice;
        }

        private GraphicsDeviceManager m_DeviceManager;

        RenderDevice()
        {
            //m_DeviceManager = new GraphicsDeviceManager(
        }

        public static bool IsDirectX11Supported()
        {
            return SharpDX.Direct3D11.Device.GetSupportedFeatureLevel() == FeatureLevel.Level_11_1;
        }
    };
};