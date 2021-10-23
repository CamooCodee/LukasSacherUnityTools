using static System.IO.Path;
using static System.IO.Directory;
using static UnityEngine.Application;

namespace lukassacher.UnityTools
{
    public static class Folders
    {
        public static void CreateDirectories(string root, params string[] directories)
        {
            var fullPath = Combine(dataPath, root);
            foreach (var directory in directories)
            {
                CreateDirectory(Combine(fullPath, directory));
            }
        }
    }
}