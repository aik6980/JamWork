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

using Renderer;

namespace Graphic
{
    interface ISceneView
    {
        void Init();
        void Render();
        void Destroy();
    };

    enum SCENEVIEW_TYPE : int
    {
        GBUFFER_VIEW,
        ENVIRONMENT_VIEW,
        FINALCOMPOSITION_VIEW,
        SCENEVIEW_TYPE_COUNT
    }

    class SceneViewManager : Util.Singleton<SceneViewManager>
    {
        Dictionary<SCENEVIEW_TYPE, ISceneView>      m_SceneViewMap = new Dictionary<SCENEVIEW_TYPE,ISceneView>(); 

        public void Init()
        {
            m_SceneViewMap[SCENEVIEW_TYPE.GBUFFER_VIEW]          = new GBufferView();   
            m_SceneViewMap[SCENEVIEW_TYPE.ENVIRONMENT_VIEW]      = new EnvironmentView();
            m_SceneViewMap[SCENEVIEW_TYPE.FINALCOMPOSITION_VIEW] = new FinalCompositionView();

            for (int i = 0; i < (int)SCENEVIEW_TYPE.SCENEVIEW_TYPE_COUNT; ++i)
            {
                m_SceneViewMap[(SCENEVIEW_TYPE)i].Init();
            }
        }

        public void Render()
        {
            for(int i=0; i<(int)SCENEVIEW_TYPE.SCENEVIEW_TYPE_COUNT; ++i)
            {
                m_SceneViewMap[(SCENEVIEW_TYPE)i].Render();
            }
        }

        public void Destroy()
        {
            for (int i = 0; i < (int)SCENEVIEW_TYPE.SCENEVIEW_TYPE_COUNT; ++i)
            {
                m_SceneViewMap[(SCENEVIEW_TYPE)i].Destroy();
            }
        }

        public T GetView<T>(SCENEVIEW_TYPE svt) where T : class
        {
            T view  = m_SceneViewMap[svt] as T;
            return view;

        }
    };
}