using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class DialogText : MonoBehaviour
{
    public static List<DialogTextRepository> dialogTextRepository=new List<DialogTextRepository>();
    public static Dictionary<string, float> coffeeResultDict = new Dictionary<string, float>();
    private List<string> _path = new List<string>(){
        "\\DialogInfo\\1-1Text.json",
        "\\DialogInfo\\1-2Text.json",
    };

    private void Awake() 
    {
        for(int i=0;i<_path.Count;i++)
        {
            string data = ReadJsonUlilities.ReadJson(_path[i]);
            dialogTextRepository.Add(JsonConvert.DeserializeObject<DialogTextRepository>(data));
        }

        coffeeResultDict=JsonConvert.DeserializeObject<Dictionary<string, float>>(ReadJsonUlilities.ReadJson("\\DialogInfo\\Dictionary.json"));

        // Debug.Log(coffeeResultDict.Count);
    }
}
