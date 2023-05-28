using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class NewsDirector : MonoBehaviour
{
    [Header("NewsContent Settings")]
    public Transform newsPanel;
    public TextMeshProUGUI newsContent;
    public List<TextMeshProUGUI> newsButtonContentList;
    
    private bool _isShowNews;
    private NewsList _newsList;
    private const string Path = "/DialogInfo/News.json";

    private void Start()
    {
        //Read from json
        string data =ReadJsonUlilities.ReadJson(Path);
        _newsList = JsonConvert.DeserializeObject<NewsList>(data);

        // foreach(var a in _newsList.NewsData)
        // {
        //     Debug.Log(a.NewsID);
        //     Debug.Log(a.NewsTittle);
        //     Debug.Log(a.NewsBody);
        // }

        for (int i = 0; i < newsButtonContentList.Count; i++)
        {
            newsButtonContentList[i].text = _newsList.NewsData[i].NewsTittle;
        }
    }

    public void ShowNewsContent(int index)
    {
        newsContent.text = _newsList.NewsData[index].NewsBody;
    }

    public void ShowOrCloseNews()
    {
        _isShowNews=!_isShowNews;
        newsPanel.gameObject.SetActive(_isShowNews);
    }

}
