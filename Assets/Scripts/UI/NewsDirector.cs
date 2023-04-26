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
    [HideInInspector] public GameObject NewsContent;
    [HideInInspector] public List<Transform> NewsButtonList;
    private bool _isShowNews;
    private NewsList _newsList;
    private const string Path = "\\DialogInfo\\News.json";

    private void Start()
    {
        //TODO: read from json
        string data = ReadJson();
        _newsList = JsonConvert.DeserializeObject<NewsList>(data);

        // foreach(var a in _newsList.NewsData)
        // {
        //     Debug.Log(a.NewsID);
        //     Debug.Log(a.NewsTittle);
        //     Debug.Log(a.NewsBody);
        // }

        NewsButtonList = new List<Transform>();

        for (int i = 0; i < NewsPanel.childCount; i++)
        {
            NewsButtonList.Add(NewsPanel.GetChild(i));

            //FIXME: 修改界限
            if (i > 0 && i < 4)
                NewsButtonList[i].GetChild(0).GetComponent<TextMeshProUGUI>().text = _newsList.NewsData[i-1].NewsTittle;
        }
    }

    #region 没想好怎么写，先随便乱写的
    public void ShowNewsContent0()
    {
        NewsButtonList[0].GetChild(0).GetComponent<TextMeshProUGUI>().text = _newsList.NewsData[0].NewsBody;
    }
    public void ShowNewsContent1()
    {
        NewsButtonList[0].GetChild(0).GetComponent<TextMeshProUGUI>().text = _newsList.NewsData[1].NewsBody;
    }

    public void ShowNewsContent2()
    {
        NewsButtonList[0].GetChild(0).GetComponent<TextMeshProUGUI>().text = _newsList.NewsData[2].NewsBody;
    }
    public void ShowNewsContent3()
    {
        NewsButtonList[0].GetChild(0).GetComponent<TextMeshProUGUI>().text = _newsList.NewsData[3].NewsBody;
    }
    public void ShowNewsContent4()
    {
        NewsButtonList[0].GetChild(0).GetComponent<TextMeshProUGUI>().text = _newsList.NewsData[4].NewsBody;
    }
    public void ShowNewsContent5()
    {
        NewsButtonList[0].GetChild(0).GetComponent<TextMeshProUGUI>().text = _newsList.NewsData[5].NewsBody;
    }
    #endregion

    private string ReadJson()
    {
        string jsonData;
        string fileUrl = Application.dataPath + Path;

        using (StreamReader sr = File.OpenText(fileUrl))
        {
            //数据保存
            jsonData = sr.ReadToEnd();
            sr.Close();
        }

        // Debug.Log(jsonData);
        return jsonData;
    }

    public void ShowOrCloseNews()
    {
        _isShowNews=!_isShowNews;
        NewsPanel.gameObject.SetActive(_isShowNews);
    }

}
