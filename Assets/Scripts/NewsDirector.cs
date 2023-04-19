using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class NewsDirector : MonoBehaviour
{
    private NewsList _newsList;

    private void Awake() 
    {
        // //TODO: read from json
        // string newsData=ReadJson();
        // _newsList=JsonConvert.DeserializeObject<NewsList>(newsData);

        // if(_newsList.newsData.Count==0)
        //     Debug.LogWarning("read newsdata failed");

    }
    private string ReadJson()
    {
        string jsonData;
        string fileUrl = Application.dataPath+"\\a.json";

        using (StreamReader sr =File.OpenText(fileUrl))
        {
            //数据保存
            jsonData = sr.ReadToEnd();
            sr.Close();
        }

        Debug.Log(jsonData);
        return jsonData;
    }
}
