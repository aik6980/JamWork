using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphic
{
     class GBufferView : BaseSceneView
     {
        public override SCENEVIEW_TYPE SceneViewType()
        {
            return SCENEVIEW_TYPE.GBUFFER_VIEW;
        }

         public override void Init()
         {}

         public override void Destroy()
         {}

         public override void Render()
         {
             RenderToView();
         }
     };
}
