using System.Runtime.InteropServices;

using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;

using Renderer;

namespace Graphic
{
     class SceneFilterView : BaseSceneView
     {
        public override SCENEVIEW_TYPE SceneViewType()
        {
            return SCENEVIEW_TYPE.SCENEVIEW_TYPE_COUNT;
        }

         public override void Init()
         {}

         public override void Destroy()
         {}

         public override void Render()
         {}
     };
}