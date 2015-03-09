using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectGroup
{
    interface IObjectGroup
    {
        void Init();
        void Update();
        void Render();
        void Destroy();
    }

    enum ObjectGroupType : int
    {
        SPRITEGROUP,
        OBJECTGROUP_TYPE_COUNT
    }

    class CObjectGroupManager : Util.Singleton<CObjectGroupManager>
    {
        public void Init()
        {

        }


        Dictionary<ObjectGroupType, IObjectGroup> m_ObjectGroups = new Dictionary<ObjectGroupType, IObjectGroup>(); 
    }
}
