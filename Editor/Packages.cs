#if UNITY_EDITOR
using System.Net.Http;
using System.Threading.Tasks;
using static System.IO.File;
using static System.IO.Path;
using static UnityEditor.PackageManager.Client;
using static UnityEngine.Application;

namespace lukassacher.UnityTools
{
    public static class Packages
    {
        public static async void ReplacePackageFileByGistContent(string id, string user = "CamooCodee")
        {
            var url = GetGistUrl(user, id);
            var contents = await GetContents(url);
            ReplacePackageFile(contents);
        }

        public static void InstallUnityPackage(string name) => Add($"com.unity.{name}");

        private static string GetGistUrl(string user, string id) => $"https://gist.githubusercontent.com/{user}/{id}/raw";

        private static async Task<string> GetContents(string url)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            var content = response.Content.ReadAsStringAsync();
            return await content;
        }

        private static void ReplacePackageFile(string contents)
        {
            var existing = GetManifestPath();
            WriteAllText(existing, contents);
            Resolve();
        }

        private static string GetManifestPath() => Combine(dataPath, "../Packages/manifest.json");
    }
}
#endif