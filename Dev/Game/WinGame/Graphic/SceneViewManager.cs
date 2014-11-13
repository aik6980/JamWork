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

    enum SceneViewType : int
    {
        ENVIRONMENT_VIEW,
        FINALCOMPOSITION_VIEW,
        NUM_VIEWS
    }

    class SceneViewManager : Util.Singleton<SceneViewManager>
    {
        Dictionary<SceneViewType, ISceneView>      m_SceneViewMap = new Dictionary<SceneViewType,ISceneView>(); 

        public void Init()
        {
            m_SceneViewMap[SceneViewType.ENVIRONMENT_VIEW]      = new EnvironmentView();
            m_SceneViewMap[SceneViewType.FINALCOMPOSITION_VIEW] = new FinalCompositionView();

            for (int i = 0; i < (int)SceneViewType.NUM_VIEWS; ++i)
            {
                m_SceneViewMap[(SceneViewType)i].Init();
            }
        }

        public void Render()
        {
            for(int i=0; i<(int)SceneViewType.NUM_VIEWS; ++i)
            {
                m_SceneViewMap[(SceneViewType)i].Render();
            }
        }

        public void Destroy()
        {
            for (int i = 0; i < (int)SceneViewType.NUM_VIEWS; ++i)
            {
                m_SceneViewMap[(SceneViewType)i].Destroy();
            }
        }

        public T GetView<T>(SceneViewType sceneType) where T : class
        {
            T view  = m_SceneViewMap[sceneType] as T;
            return view;

        }
    };
}