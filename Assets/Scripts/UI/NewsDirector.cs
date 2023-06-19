using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using Unity.VisualScripting;

namespace UI
{
    public class NewsDirector : MonoBehaviour
    {
        [Header("NewsContent Settings")]
        public Transform newsPanel;
        public TextMeshProUGUI newsContent;
        public List<TextMeshProUGUI> newsButtonContentList;
        
        private NewsList _newsList;
        private const string Path = "/News.json";
    
        private void Start()
        {
            //Read from json
            var data =ReadJsonUlilities.ReadJson(Path);
            _newsList = JsonConvert.DeserializeObject<NewsList>(data);
            //Initialize the news list title
            for (var i = 0; i < newsButtonContentList.Count; i++)
            {
                newsButtonContentList[i].text = _newsList.NewsData[i].NewsTittle;
            }
            //Initialize the news content whit null
            newsContent.text = " ";
        }
        
        private void OnDisable()
        {
            foreach (var a in newsButtonContentList)
            { 
                a.transform.parent.gameObject.SetActive(false);
            }
            
        }
        /// <summary>
        /// initialize the news list whit the number of news which is saved in GameManager
        /// </summary>
        public void InitializedNewsList()
        {
            for(var i=0;i<GameManager.Instance.newsNum;i++)
            {
                newsButtonContentList[i].transform.parent.gameObject.SetActive(true);
            }
        }
        
        public void ShowNewsContent(int index)
        {
            newsContent.text = _newsList.NewsData[index].NewsBody;
        }
        
    }
}

