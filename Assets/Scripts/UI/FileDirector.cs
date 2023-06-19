using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Tools;
using TMPro;

namespace UI
{
    public class FileDirector : MonoBehaviour
    {
        [Header("NewsContent Settings")]
        public Transform filePanel;
        public TextMeshProUGUI fileContent;
        public List<TextMeshProUGUI> fileButtonContentList;
        
        private const string FilePath="/File.json";
        private FileDataList _fileDataList;

        private void Start()
        {
            var data = ReadJsonUlilities.ReadJson(FilePath);
            _fileDataList = JsonConvert.DeserializeObject<FileDataList>(data);

            //Initialize the file list title
            for (var i = 0; i < fileButtonContentList.Count; i++)
            {
                fileButtonContentList[i].text = _fileDataList.Data[i].Title;
            }
            //Initialize the file content whit null
            fileContent.text = " ";
        }
        private void OnDisable()
        {
            foreach (var a in fileButtonContentList)
            { 
                a.transform.parent.gameObject.SetActive(false);
            }
            
        }
        /// <summary>
        /// initialize the file list whit the number of news which is saved in GameManager
        /// </summary>
        public void InitializedNewsList()
        {
            for(var i=0;i<GameManager.Instance.fileNum;i++)
            {
                fileButtonContentList[i].transform.parent.gameObject.SetActive(true);
            }
        }
        
        public void ShowNewsContent(int index)
        {
            fileContent.text = _fileDataList.Data[index].Content;
        }

    }}

