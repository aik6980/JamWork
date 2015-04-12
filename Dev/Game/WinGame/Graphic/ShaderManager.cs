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

    class RenderShaderEntryInfo
    {
        public RenderShaderEnum m_ShaderEnum;
        public string           m_VSFileName = "";
        public string           m_GSFileName = "";
        public string           m_PSFileName = "";

        public RenderShaderEntryInfo( RenderShaderEnum shaderEnum, string vsFn, string psFn )
        {
            m_ShaderEnum  = shaderEnum;
            m_VSFileName        = vsFn;
            m_PSFileName        = psFn;
        }

        public RenderShaderEntryInfo( RenderShaderEnum shaderEnum, string vsFn, string gsFn, string psFn )
            : this(shaderEnum, vsFn, psFn) 
        {
            m_GSFileName        = gsFn;
        }
    }

    class ComputeShaderEntryInfo
    {
        public ComputeShaderEnum    m_ShaderEnum;
        public string               m_FileName;

        public ComputeShaderEntryInfo( ComputeShaderEnum shaderEnum, string fn)
        {
            m_ShaderEnum    = shaderEnum;
            m_FileName      = fn;
        }
    }

    class RenderShaderEntry
    {
        public RenderShaderEntryInfo m_Info;

        public VertexShader     m_VS;
        public GeometryShader   m_GS;
        public PixelShader      m_PS;

        public RenderShaderEntry(RenderShaderEntryInfo i)
        {
            m_Info = i;
        }

        public void Dispose()
        {
            Util.Helper.SafeDispose(m_PS);
            Util.Helper.SafeDispose(m_GS);
            Util.Helper.SafeDispose(m_VS);
        }
    };

    class ComputeShaderEntry
    {
        public ComputeShaderEntryInfo m_Info;

        public ComputeShader m_CS;

        public ComputeShaderEntry(ComputeShaderEntryInfo i)
        {
            m_Info = i;
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
            RegisterRenderShader(new RenderShaderEntryInfo( RenderShaderEnum.ENVIRONMENT, "Shaders/fullscreen_tri_vs.cso", "Shaders/environment_ps.cso" ));
            RegisterRenderShader(new RenderShaderEntryInfo( RenderShaderEnum.FINAL_COMPOSITION, "Shaders/fullscreen_tri_vs.cso", "Shaders/final_composition_ps.cso" ));
            
            //RegisterComputeShader(new ComputeShaderEntryInfo( ComputeShaderEnum.COMPUTE0, "Shaders/compute0_cs.cso"));

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

            if(entry.m_Info.m_VSFileName.Length != 0)
            {
                var bytecode = ShaderBytecode.FromFile(entry.m_Info.m_VSFileName);
                entry.m_VS = new VertexShader( dev_context, bytecode );
            }

            if(entry.m_Info.m_GSFileName.Length != 0)
            {
                var bytecode = ShaderBytecode.FromFile(entry.m_Info.m_GSFileName);
                entry.m_GS = new GeometryShader( dev_context, bytecode );
            }

            if(entry.m_Info.m_PSFileName.Length != 0)
            {
                var bytecode = ShaderBytecode.FromFile(entry.m_Info.m_PSFileName);
                entry.m_PS = new PixelShader( dev_context, bytecode );
            }
        }

        void ReloadShader(ComputeShaderEntry entry)
        {
            entry.Dispose();
            var dev_context = Renderer.RenderDevice.Instance().Device;

            if(entry.m_Info.m_FileName.Length != 0)
            {
                var bytecode = ShaderBytecode.FromFile(entry.m_Info.m_FileName);
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

        public void RegisterRenderShader(RenderShaderEntryInfo i)
        {
           var shader = new RenderShaderEntry(i);
           m_RenderShaderList.Add(i.m_ShaderEnum, shader);
        }

        public void RegisterComputeShader(ComputeShaderEntryInfo i)
        {
           var shader = new ComputeShaderEntry(i);
           m_ComputeShaderList.Add(i.m_ShaderEnum, shader);
        }
    };
}