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

namespace Graphic
{
    class Mesh
    {
        public List<Vector3>    Positions = new List<Vector3>();
        public List<Vector3>    Normals = new List<Vector3>();
        public List<Vector2>    Texcoords = new List<Vector2>();

        public List<Int32>      Indices = new List<Int32>();

        public void Reset()
        {
            Positions.Clear();
            Normals.Clear();
            Texcoords.Clear();

            Indices.Clear();
        }
    };

    abstract class Geometry
    {
        public abstract void Generate();

        public Mesh m_Mesh = new Mesh();
    };

    class Box
    {
    
    };

    class Triangle : Geometry
    {
        public override void Generate()
        {
            m_Mesh.Reset();

            Vector3 center_of_mass = new Vector3(0.0f, 0.333f, 0.0f);
            m_Mesh.Positions.Add(new Vector3(-1.0f, 0.0f, 0.0f ) - center_of_mass);
            m_Mesh.Positions.Add(new Vector3( 0.0f, 1.0f, 0.0f ) - center_of_mass);   
            m_Mesh.Positions.Add(new Vector3( 1.0f, 0.0f, 0.0f ) - center_of_mass);

            Int32[] idx_arry = {0,1,2};
            m_Mesh.Indices.AddRange( idx_arry );
        }
    };

    class Square
    {
        
    };

    // http://paulbourke.net/geometry/platonic/
    class Tetrahedron : Geometry
    {
        public override void Generate()
        {
            m_Mesh.Reset();

            Vector3 center_of_mass = new Vector3(0.0f, 0.333f, 0.0f);
            m_Mesh.Positions.Add(new Vector3(-1.0f, 0.0f, 0.0f ) - center_of_mass);
            m_Mesh.Positions.Add(new Vector3( 0.0f, 1.0f, 0.0f ) - center_of_mass);   
            m_Mesh.Positions.Add(new Vector3( 1.0f, 0.0f, 0.0f ) - center_of_mass);
        }
    }
}
