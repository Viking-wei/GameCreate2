using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public static class ReadJsonUlilities
{
    public static string ReadJson(string path)
    {
        string jsonData;
        string fileUrl = Application.streamingAssetsPath + path;

        using (StreamReader sr = File.OpenText(fileUrl))
        {
            //数据保存
            jsonData = sr.ReadToEnd();
            sr.Close();
        }

        // Debug.Log(jsonData);
        return jsonData;
    }
}