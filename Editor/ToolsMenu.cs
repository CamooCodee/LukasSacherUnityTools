using System.Linq;
using UnityEditor;
using UnityEngine;
using static UnityEditor.AssetDatabase;
using static lukassacher.UnityTools.Folders;
using static lukassacher.UnityTools.Packages;

namespace lukassacher.UnityTools
{
    public static class ToolsMenu
    {
        [MenuItem("Tools/Setup/Create Default Folders")]
        private static void CreateDefaultFolders()
        {
            CreateDirectories("_Project", "Scripts", "Scenes", "Art");
            Refresh();
        }

        [MenuItem("Tools/Setup/Packages/Bulks/Use 2D And 3D Packages", priority = 0)]
        private static void Setup2dAnd3DManifest() => ReplacePackageFileByGistContent("1efcf375b313a21127da66e0b6883f2d");

        [MenuItem("Tools/Setup/Packages/Bulks/Use 2D Packages", priority = 1)]
        private static void Setup2DManifest() => ReplacePackageFileByGistContent("466cdcf44f07cead8deebc340535656f");

        [MenuItem("Tools/Setup/Packages/Bulks/Use 3D Packages", priority = 2)]
        private static void Setup3DManifest() => ReplacePackageFileByGistContent( "fc549fc1c9b4c84555644735593bd99b");

        private const string SETUP_TOOLS_MANAGER_UNDO = "Setup Tools Manager";
        
        [MenuItem("Tools/Setup/Setup Tools Manager", true)]
        private static bool SetupToolsManagerValidate()
        {
            return Object.FindObjectOfType<ToolsManager>(true) == null;
        }
        
        [MenuItem("Tools/Setup/Setup Tools Manager")]
        private static void SetupToolsManager()
        {
            var go = new GameObject("----- Tools Manager -----");
            go.AddComponent<ToolsManager>();
            go.transform.SetAsFirstSibling();
            Undo.RegisterCreatedObjectUndo(go, SETUP_TOOLS_MANAGER_UNDO);
        }
        
        [MenuItem("Tools/Setup/Packages/New Input System")]
        private static void AddNewInputSystem() => InstallUnityPackage("inputsystem");
        
        [MenuItem("Tools/Setup/Packages/Post Processing")]
        private static void AddPostPro() => InstallUnityPackage("postprocessing");
        
        [MenuItem("Tools/Setup/Packages/Cinemachine")]
        private static void AddCinemachine() => InstallUnityPackage("cinemachine");
    }
}
