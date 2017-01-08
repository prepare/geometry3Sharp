﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace g3
{
    public interface IMesh
    {
        int VertexCount { get; }
        int TriangleCount { get; }
		int MaxVertexID { get; }
		int MaxTriangleID { get; }

        bool HasVertexColors { get; }
        bool HasVertexNormals { get; }
        bool HasVertexUVs { get; }

        Vector3d GetVertex(int i);
        Vector3f GetVertexNormal(int i);
        Vector3f GetVertexColor(int i);
        Vector2f GetVertexUV(int i);
		NewVertexInfo GetVertexAll(int i);


        bool HasTriangleGroups { get; }

        Index3i GetTriangle(int i);
        int GetTriangleGroup(int i);


        // iterators allow us to work with gaps in index space
        System.Collections.Generic.IEnumerable<int> VertexIndices();
        System.Collections.Generic.IEnumerable<int> TriangleIndices();

    }


    /*
     * Abstracts construction of meshes, so that we can construct different types, etc
     */
    public struct NewVertexInfo
    {
        public Vector3d v;
        public Vector3f n, c;
        public Vector2f uv;
        public bool bHaveN, bHaveUV, bHaveC;

		public NewVertexInfo(Vector3d v) {
			this.v = v; n = c = Vector3f.Zero; uv = Vector2f.Zero;
			bHaveN = bHaveC = bHaveUV = false;
		}
		public NewVertexInfo(Vector3d v, Vector3f n) {
			this.v = v; this.n = n; c = Vector3f.Zero; uv = Vector2f.Zero;
			bHaveN = true; bHaveC = bHaveUV = false;
		}
		public NewVertexInfo(Vector3d v, Vector3f n, Vector3f c) {
			this.v = v; this.n = n; this.c = c; uv = Vector2f.Zero;
			bHaveN = bHaveC = true; bHaveUV = false;
		}
		public NewVertexInfo(Vector3d v, Vector3f n, Vector3f c, Vector2f uv) {
			this.v = v; this.n = n; this.c = c; this.uv = uv;
			bHaveN = bHaveC = bHaveUV = true;
		}
    }
    public interface IMeshBuilder
    {
        // return ID of new mesh
        int AppendNewMesh(bool bHaveVtxNormals, bool bHaveVtxColors, bool bHaveVtxUVs, bool bHaveFaceGroups);
        void SetActiveMesh(int id);

        int AppendVertex(double x, double y, double z);
        int AppendVertex(NewVertexInfo info);

        int AppendTriangle(int i, int j, int k);
        int AppendTriangle(int i, int j, int k, int g);


        // material handling

        // return client-side unique ID of material
        int BuildMaterial(GenericMaterial m);

        // do material assignment to mesh, where meshID comes from IMeshBuilder
        void AssignMaterial(int materialID, int meshID);
    }




}
