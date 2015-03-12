using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectGroup
{
    class VFXObjectGroup : IObjectGroup
    {
        public static readonly ObjectGroupType OBJECTGROUP_TYPE = ObjectGroupType.OBJECTGROUP_VFX;
        public ObjectGroupType Type() { return OBJECTGROUP_TYPE; }

        public void Init()
        {
            
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
    }
}
