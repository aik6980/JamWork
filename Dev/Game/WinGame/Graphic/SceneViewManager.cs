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

    enum SceneViewType
    {
        ENVIRONMENT_VIEW,
        FINALCOMPOSITION_VIEW,
    }

    class SceneViewManager : Util.Singleton<SceneViewManager>
    {
        Dictionary<string, ISceneView>      m_SceneViewMap; 

        public void Init()
        {

        }
    };
}