using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Tools;

namespace UI
{
    public class FileDirector : MonoBehaviour
    {
        private const string FilePath="/DialogInfo/File.json";
        private FileDataList _fileDataList;

        private void Start()
        {
            var data = ReadJsonUlilities.ReadJson(FilePath);
            _fileDataList = JsonConvert.DeserializeObject<FileDataList>(data);

            // foreach (var dl in _fileDataList.Data)
            // {
            //     Debug.Log(dl.ID);
            //     Debug.Log(dl.Title);
            //     Debug.Log(dl.Content);
            // }
        }
    }
}

