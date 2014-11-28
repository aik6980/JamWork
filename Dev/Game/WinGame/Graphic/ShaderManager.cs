using System.Diagnostics;
using System.Collections.Generic;

using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;

using SharpDX.Multimedia;
using SharpDX.RawInput;
using SharpDX.DirectInput;

namespace Graphic
{
    enum RenderShaderEnum
    {
        ENVIRONMENT,
        FINAL_COMPOSITION,
        NUM_RENDERSHADER
    };

    enum ComputeShaderEnum
    {
        COMPUTE0,
        NUM_COMPUTESHADER
    };

    class RenderShaderEntry
    {
        public RenderShaderEnum m_RenderShaderEnum;
        public string       m_VSFileName;
        public string       m_PSFileName;

        public VertexShader m_VS;
        public PixelShader  m_PS;

        public RenderShaderEntry( RenderShaderEnum shaderEnum, string vsFn, string psFn )
        {
            m_RenderShaderEnum = shaderEnum;
            m_VSFileName = vsFn;
            m_PSFileName = psFn;
        }

        public void Dispose()
        {
            Util.Helper.SafeDispose(m_VS);
            Util.Helper.SafeDispose(m_PS);
        }
    };

    class ComputeShaderEntry
    {
        public ComputeShaderEnum m_ComputeShaderEnum;
        public string m_CSFileName;

        public ComputeShader m_CS;

        public ComputeShaderEntry( ComputeShaderEnum shaderEnum, string fn )
        {
            m_ComputeShaderEnum = shaderEnum;
            m_CSFileName = fn;
        }

        public void Dispose()
        {
            Util.Helper.SafeDispose(m_CS);
        }
    }; 

    class ShaderManager : Util.Singleton<ShaderManager>
    {
        Dictionary<RenderShaderEnum, RenderShaderEntry> m_RenderShaderList      = new Dictionary<RenderShaderEnum,RenderShaderEntry>();
        Dictionary<ComputeShaderEnum, ComputeShaderEntry> m_ComputeShaderList   = new Dictionary<ComputeShaderEnum, ComputeShaderEntry>();

        public void Init()
        {
            m_RenderShaderList.Add( RenderShaderEnum.ENVIRONMENT, 
                new RenderShaderEntry( RenderShaderEnum.ENVIRONMENT, "Shaders/fullscreen_tri_vs.cso", "Shaders/environment_ps.cso" ));
            m_RenderShaderList.Add( RenderShaderEnum.FINAL_COMPOSITION, 
                new RenderShaderEntry( RenderShaderEnum.FINAL_COMPOSITION, "Shaders/fullscreen_tri_vs.cso", "Shaders/final_composition_ps.cso" ));

            m_ComputeShaderList.Add( ComputeShaderEnum.COMPUTE0, 
                new ComputeShaderEntry( ComputeShaderEnum.COMPUTE0, "Shaders/compute0_cs.cso" ));

            ReloadAllShaders();
        }

        public void Update()
        {
            if(Input.InputManager.Instance().KeyPressed(Key.R))
            {
                ReloadAllShaders();
            }
        }

        public void Destroy()
        {
            foreach( var kvp in m_RenderShaderList)
            {
                kvp.Value.Dispose();
            }

            foreach( var kvp in m_ComputeShaderList)
            {
                kvp.Value.Dispose();
            }
        }

        void ReloadAllShaders()
        {
            foreach( var kvp in m_RenderShaderList)
            {
                ReloadShader(kvp.Value);
            }

            foreach( var kvp in m_ComputeShaderList)
            {
                ReloadShader(kvp.Value);
            }
        }

        void ReloadShader(RenderShaderEntry entry)
        {
            entry.Dispose();
            var dev_context = Renderer.RenderDevice.Instance().Device;

            if(entry.m_VSFileName.Length != 0)
            {
                var bytecode = ShaderBytecode.FromFile(entry.m_VSFileName);
                entry.m_VS = new VertexShader( dev_context, bytecode );
            }

            if(entry.m_PSFileName.Length != 0)
            {
                var bytecode = ShaderBytecode.FromFile(entry.m_PSFileName);
                entry.m_PS = new PixelShader( dev_context, bytecode );
            }
        }

        void ReloadShader(ComputeShaderEntry entry)
        {
            entry.Dispose();
            var dev_context = Renderer.RenderDevice.Instance().Device;

            if(entry.m_CSFileName.Length != 0)
            {
                var bytecode = ShaderBytecode.FromFile(entry.m_CSFileName);
                entry.m_CS = new ComputeShader( dev_context, bytecode );
            }
        }

        public RenderShaderEntry Find(RenderShaderEnum key)
        {
            RenderShaderEntry val;
            var found = m_RenderShaderList.TryGetValue(key, out val);
            return val;
        }

        public ComputeShaderEntry Find(ComputeShaderEnum key)
        {
            ComputeShaderEntry val;
            var found = m_ComputeShaderList.TryGetValue(key, out val);
            return val;
        }
    };
}