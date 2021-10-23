using System.IO;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

namespace lukassacher.UnityTools
{
    public static class lukassacher_EditorUtility
    {
        public static readonly GUIStyle sectionStyle;
        public static GUIStyle sectionHeaderStyle;
        public static GUIStyle sectionPropLabelStyle;
        
        private static readonly Texture2D sectionTexture;

        private static string GetPackagePath([CallerFilePath] string filePath = "")
        {
            var fileDirPath = Path.GetDirectoryName(filePath);
            var packagePath = Path.GetFullPath(Path.Combine(fileDirPath, @"..\"));
            
            if (Directory.Exists(packagePath))
                return packagePath;
            
            Debug.LogError("UnityUtility: Cannot find package path.");
            return "";
        }

        static lukassacher_EditorUtility()
        {
            string packetDirInProject = Path.Combine(Application.dataPath, "lukassacher-Tools");

            if(!Directory.Exists(packetDirInProject)) Directory.CreateDirectory(packetDirInProject);

            string sectionTexturePath;

            if (EditorGUIUtility.isProSkin)
                sectionTexturePath = "Packages/com.lukassacher.tools/Editor/Textures/Section_Dark.png";
            else
                sectionTexturePath = "Packages/com.lukassacher.tools/Editor/Textures/Section_Light.png";
            
            sectionTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(sectionTexturePath);
            
            sectionStyle = new GUIStyle(EditorStyles.label) { richText = true, border = new RectOffset(9, 9, 9, 9), padding = new RectOffset(15, 10, 3, 7)};
            sectionStyle.normal.background = sectionTexture;

            sectionHeaderStyle = new GUIStyle(EditorStyles.label) { richText = true, fontStyle = FontStyle.Bold, padding = new RectOffset(5, 0, 0, 3)};
            sectionPropLabelStyle = new GUIStyle(EditorStyles.label) { richText = true, padding = new RectOffset(15, 0, 1, 1) };
        }

        private static GUIStyle _cachedLabelStyle;
        public static void SetLabelStyle(GUIStyle style)
        {
            _cachedLabelStyle = EditorStyles.label;
            EditorStyles.label.overflow = style.overflow;
            EditorStyles.label.richText = style.richText;
            EditorStyles.label.padding = style.padding;
        }

        public static void ResetLabelStyle()
        {
            EditorStyles.label.overflow = _cachedLabelStyle.overflow;
            EditorStyles.label.richText = _cachedLabelStyle.richText;
            EditorStyles.label.padding = _cachedLabelStyle.padding;
        }
    }
}