using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ObjectGroup;

namespace Graphic
{
    abstract class BaseSceneView : ISceneView
    {
        protected List<IObjectGroup> m_ObjectGroups = new List<IObjectGroup>();

        abstract public SCENEVIEW_TYPE SceneViewType();
        abstract public void Init();
        abstract public void Render();
        abstract public void Destroy();

        public void Register(IObjectGroup objgrp)
        {
            var b = m_ObjectGroups.Exists( x => x.GetType() == objgrp.GetType() );
            if(!b)
            {
                m_ObjectGroups.Add(objgrp);
            }
        }

        protected void RenderToView()
        {
            foreach( var it in m_ObjectGroups)
            {
                var svt = SceneViewType();
                it.Render(svt);
            }
        }
    }
}
