using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AssetImporters;
using System.IO;
using PlasticGui.Configuration.CloudEdition.Welcome;
using System.Linq;
using System;

namespace QSTX.RdcMeshImporter
{
    [ScriptedImporter(1, "rdc_mesh")]
    public class RdcMeshImporter : ScriptedImporter
    {
        public string header = "";
        public string[] tags = new string[2] { "Zero", "One" };
        public int posXTagIdx = 0;
        public int posYTagIdx = 0;
        public int posZTagIdx = 0;

        private int[] indices = null;
        float[][] datas = null;
        int vertexNum = 0;
        public override void OnImportAsset(AssetImportContext ctx)
        {
            string[] allLines = File.ReadAllLines(ctx.assetPath);
            header = allLines[0];
            GetTags();
                
            indices = new int[allLines.Length - 1];
            datas = new float[tags.Length-2][];
            
            string[] dataLines = new string[allLines.Length - 1];
            int maxIndex = 0;
            for(int line=0; line<allLines.Length - 1; ++line)
            {
                string[] strs = allLines[line + 1].Split(',', 3);
                int index=int.Parse(strs[1]);
                indices[line] = index;
                maxIndex=Math.Max(maxIndex, index);
                dataLines[index] = strs[2];
            }

            vertexNum = maxIndex + 1;
            for (int i = 0; i < datas.Length; ++i)
            {
                datas[i] = new float[vertexNum];
            }

            for(int index=0;index< vertexNum; ++index)
            {
                string[] strs = dataLines[index].Split(',');

                for (int i = 0; i < strs.Length; ++i)
                {
                    datas[i][index] = float.Parse(strs[i]);
                }
            }
            GetData(ctx);
            
        }
        private void GetTags()
        {
            string[] strs = header.Split(',');
            tags = new string[strs.Length];
            tags[0] = "Zero";
            tags[1] = "One";
            for(int i = 2; i < strs.Length; ++i)
            {
                tags[i]=strs[i].Trim();
            }
        }
        private void GetData(AssetImportContext ctx)
        {
            Mesh mesh = new Mesh();
            Vector3[] vertices = new Vector3[vertexNum];
            for(int i = 0;i< vertexNum; ++i)
            {
                vertices[i].x = (posXTagIdx < 2 ? posXTagIdx : datas[posXTagIdx - 2][i]);
                vertices[i].y = (posYTagIdx < 2 ? posYTagIdx : datas[posYTagIdx - 2][i]);
                vertices[i].z = (posZTagIdx < 2 ? posZTagIdx : datas[posZTagIdx - 2][i]);
            }
            mesh.SetVertices(vertices);
            mesh.SetIndices(indices,MeshTopology.Triangles,0);
            ctx.AddObjectToAsset("mesh", mesh);
            ctx.SetMainObject(mesh);
        }
    }
}
