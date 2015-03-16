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

using Buffer = SharpDX.Direct3D11.Buffer;

namespace ObjectGroup
{
    class VFXObjectGroup : IObjectGroup
    {
        Buffer  m_Buffer;

        public static readonly ObjectGroupType OBJECTGROUP_TYPE = ObjectGroupType.OBJECTGROUP_VFX;
        public ObjectGroupType Type() { return OBJECTGROUP_TYPE; }

        public void Init()
        {
            var view = Graphic.SceneViewManager.Instance().GetView<Graphic.GBufferView>(Graphic.SCENEVIEW_TYPE.GBUFFER_VIEW);
            view.Register(this);

            // create this object group resources
            CreateInstanceBuffer();
        }

        public void Destroy()
        {

        }

        public void Update()
        {

        }

        public void Render(Graphic.SCENEVIEW_TYPE svt)
        {

        }

        void CreateInstanceBuffer()
        {
            var dev_context = Renderer.RenderDevice.Instance().Device;

            var buffer = new Buffer(dev_context, 128, ResourceUsage.Default, 
                BindFlags.VertexBuffer|BindFlags.ShaderResource|BindFlags.UnorderedAccess , 
                CpuAccessFlags.None, ResourceOptionFlags.BufferAllowRawViews, 32);

            var buffer1 = new Buffer(dev_context, 128, ResourceUsage.Default, 
                BindFlags.UnorderedAccess , 
                CpuAccessFlags.None, ResourceOptionFlags.BufferAllowRawViews|ResourceOptionFlags.DrawIndirectArguments, 32);
        }
    }
}
