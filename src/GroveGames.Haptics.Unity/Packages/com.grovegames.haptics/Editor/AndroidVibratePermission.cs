#if UNITY_ANDROID
using System.IO;
using System.Xml;

using UnityEditor.Android;

namespace GroveGames.Haptics.Unity.Editor
{
    internal sealed class AndroidVibratePermission : IPostGenerateGradleAndroidProject
    {
        private const string AndroidNamespace = "http://schemas.android.com/apk/res/android";
        private const string Permission = "android.permission.VIBRATE";

        public int callbackOrder => 0;

        public void OnPostGenerateGradleAndroidProject(string path)
        {
            var manifestPath = Path.Combine(path, "src", "main", "AndroidManifest.xml");
            var document = new XmlDocument();
            document.Load(manifestPath);

            var manifest = document.DocumentElement;

            foreach (XmlNode node in manifest.SelectNodes("uses-permission"))
            {
                if (node is XmlElement element && element.GetAttribute("name", AndroidNamespace) == Permission)
                {
                    return;
                }
            }

            var permission = document.CreateElement("uses-permission");
            permission.SetAttribute("name", AndroidNamespace, Permission);
            manifest.PrependChild(permission);
            document.Save(manifestPath);
        }
    }
}
#endif
