using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class NewsDirector : MonoBehaviour
{
    public Transform NewsPanel;
    public TextMeshProUGUI NewsContent;
    [HideInInspector] public List<Transform> NewsButtonList;
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

        NewsButtonList = new List<Transform>();

        for (int i = 1; i < NewsPanel.childCount; i++)
        {
            NewsButtonList.Add(NewsPanel.GetChild(i));
            //FIXME: 修改界限
            if (i > 0 && i < 4)
                NewsButtonList[i].GetChild(0).GetComponent<TextMeshProUGUI>().text = _newsList.NewsData[i-1].NewsTittle;
        }
    }

    public void ShowNewsContent(int index)
    {
        NewsButtonList[0].GetChild(0).GetComponent<TextMeshProUGUI>().text = _newsList.NewsData[index].NewsBody;
    }

    public void ShowOrCloseNews()
    {
        _isShowNews=!_isShowNews;
        NewsPanel.gameObject.SetActive(_isShowNews);
    }

}
