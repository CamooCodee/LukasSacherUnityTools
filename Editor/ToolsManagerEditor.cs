#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using static lukassacher.UnityTools.lukassacher_EditorUtility;
// ReSharper disable InconsistentNaming

namespace lukassacher.UnityTools
{
    [CustomEditor(typeof(ToolsManager))]
    public class ToolsManagerEditor : Editor
    {
        private SerializedObject so;
        private SerializedProperty prop_requiredLogColor;
        private SerializedProperty prop_useLogTypeStandard;
        private SerializedProperty prop_requiredLogType;
        
        private void OnEnable()
        {
            so = serializedObject;
            prop_requiredLogColor = so.FindProperty("requiredLogColor");
            prop_useLogTypeStandard = so.FindProperty("useLogTypeStandard");
            prop_requiredLogType = so.FindProperty("requiredLogType");
        }

        public override void OnInspectorGUI()
        {
            so.Update();
            
            using (new GUILayout.VerticalScope(sectionStyle))
            {
                GUILayout.Space(5f);
                GUILayout.Label("Required Attribute Settings: ", sectionHeaderStyle);
                
                SetLabelStyle(sectionPropLabelStyle);

                GUI.enabled = !prop_useLogTypeStandard.boolValue;
                EditorGUILayout.PropertyField(prop_requiredLogColor);
                GUI.enabled = true;
                EditorGUILayout.PropertyField(prop_useLogTypeStandard);
                EditorGUILayout.PropertyField(prop_requiredLogType);
                
                ResetLabelStyle();
            }

            so.ApplyModifiedProperties();
        }
    }
}
#endif
