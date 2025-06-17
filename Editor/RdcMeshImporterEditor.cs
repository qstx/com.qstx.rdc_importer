using System.Collections;
using System.Collections.Generic;
using QSTX.RdcMeshImporter;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEditor.TerrainTools;
using UnityEngine;

namespace QSTX.RdcMeshImporter.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(RdcMeshImporter))]
    public class RdcMeshImporterEditor : ScriptedImporterEditor
    {
        private SerializedProperty tagsProp = null;
        private string[] tags = null;

        private SerializedProperty posXTagProp = null;
        private SerializedProperty posYTagProp = null;
        private SerializedProperty posZTagProp = null;
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
        }
        public override void OnInspectorGUI()
        {
            DrawTagPopup("position.x", posXTagProp);
            DrawTagPopup("position.y", posYTagProp);
            DrawTagPopup("position.z", posZTagProp);
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
