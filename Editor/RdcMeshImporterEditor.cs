using System.Collections;
using System.Collections.Generic;
using QSTX.RdcMeshImporter;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEditor.TerrainTools;
using UnityEngine;

namespace QSTX.RdcMeshImporter.Editor
{
    [CustomEditor(typeof(RdcMeshImporter))]
    public class RdcMeshImporterEditor : ScriptedImporterEditor
    {
        private SerializedProperty tagsProp = null;
        private string[] tags = null;

        private SerializedProperty posXTagProp = null;
        private SerializedProperty posYTagProp = null;
        private SerializedProperty posZTagProp = null;

        private SerializedProperty norXTagProp = null;
        private SerializedProperty norYTagProp = null;
        private SerializedProperty norZTagProp = null;

        private SerializedProperty uv0XTagProp = null;
        private SerializedProperty uv0YTagProp = null;
        private SerializedProperty uv0ZTagProp = null;
        private SerializedProperty uv0WTagProp = null;
        public override void OnEnable()
        {
            base.OnEnable();

            tagsProp = serializedObject.FindProperty("tags");
            tags= new string[tagsProp.arraySize];
            for(int i = 0; i < tags.Length; i++)
                tags[i]=tagsProp.GetArrayElementAtIndex(i).stringValue;
            posXTagProp = serializedObject.FindProperty("posXTagIdx");
            posYTagProp = serializedObject.FindProperty("posYTagIdx");
            posZTagProp = serializedObject.FindProperty("posZTagIdx");

            norXTagProp = serializedObject.FindProperty("norXTagIdx");
            norYTagProp = serializedObject.FindProperty("norYTagIdx");
            norZTagProp = serializedObject.FindProperty("norZTagIdx");

            uv0XTagProp = serializedObject.FindProperty("uv0XTagIdx");
            uv0YTagProp = serializedObject.FindProperty("uv0YTagIdx");
            uv0ZTagProp = serializedObject.FindProperty("uv0ZTagIdx");
            uv0WTagProp = serializedObject.FindProperty("uv0WTagIdx");
        }
        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("Vertices");
            DrawTagPopup("position.x", posXTagProp);
            DrawTagPopup("position.y", posYTagProp);
            DrawTagPopup("position.z", posZTagProp);
            EditorGUILayout.Space(6);

            EditorGUILayout.LabelField("Normals");
            DrawTagPopup("normal.x", norXTagProp);
            DrawTagPopup("normal.y", norYTagProp);
            DrawTagPopup("normal.z", norZTagProp);
            EditorGUILayout.Space(6);

            EditorGUILayout.LabelField("UV0s");
            DrawTagPopup("uv0.x", uv0XTagProp);
            DrawTagPopup("uv0.y", uv0YTagProp);
            DrawTagPopup("uv0.z", uv0ZTagProp);
            DrawTagPopup("uv0.w", uv0WTagProp);
            EditorGUILayout.Space(6);

            serializedObject.ApplyModifiedProperties();

            base.ApplyRevertGUI();
        }
        public void DrawTagPopup(string name, SerializedProperty tagProp)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(name, GUILayout.Width(100));
            tagProp.intValue = EditorGUILayout.Popup(tagProp.intValue, tags);
            EditorGUILayout.EndHorizontal();
        }
    }
}
