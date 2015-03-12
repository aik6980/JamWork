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
        void Render(Graphic.SCENEVIEW_TYPE svt);
        void Destroy();

        ObjectGroupType Type();
    }

    enum ObjectGroupType : int
    {
        OBJECTGROUP_VFX,
        OBJECTGROUP_TYPE_COUNT
    }

    class CObjectGroupManager : Util.Singleton<CObjectGroupManager>
    {
        Dictionary<ObjectGroupType, IObjectGroup> m_ObjectGroups = new Dictionary<ObjectGroupType, IObjectGroup>();

        public void Init()
        {
            Register();

            foreach( var kvp in m_ObjectGroups)
            {
                kvp.Value.Init();
            }
        }

        public void Update()
        {
            foreach( var kvp in m_ObjectGroups)
            {
                kvp.Value.Update();
            }
        }

        void Register()
        {
            m_ObjectGroups.Add(VFXObjectGroup.OBJECTGROUP_TYPE, new VFXObjectGroup());
        }
    }
}
