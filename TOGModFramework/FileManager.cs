using System.IO;
using UnityEngine;

namespace TOGModFramework
{
    public class FileManager : MonoBehaviour
    {
        public static string GetModJsonData(string modName, string jsonFilePath = "Settings.json")
        {
            return File.ReadAllText(Application.streamingAssetsPath +
                                    "/Mods/" + modName + "/" + jsonFilePath);
        }
    }
}