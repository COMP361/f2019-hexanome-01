using UnityEngine;
using System.IO;

public class FileManager
{
    /// <summary>
    /// Load File
    /// </summary>
    /// <typeparam name="T">Data Model Type</typeparam>
    /// <param name="filename">File Name</param>
    /// <returns>Instance</returns>
    public static T Load<T>(string filename) where T : new() {
        string directoryPath = Application.streamingAssetsPath;
        string filePath = Path.Combine(directoryPath + "/Saves/", filename);
        T output;
        if (File.Exists(filePath)) {
            string dataAsJson = File.ReadAllText(filePath);
            output = JsonUtility.FromJson<T>(dataAsJson);
        } else {
            Debug.Log("file not found");
            output = new T();
        }

        return output;
    }

    /// <summary>
    /// Save File
    /// </summary>
    /// <typeparam name="T">Model Type</typeparam>
    /// <param name="filename">File Name</param>
    /// <param name="content">Model Content</param>
    public static void Save<T>(string filename, T content) {
        string directoryPath = Application.streamingAssetsPath;
        string filePath = Path.Combine(directoryPath, filename);

        string dataAsJson = JsonUtility.ToJson(content);
        File.WriteAllText(filePath, dataAsJson);

    }
    
}