using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class DialogText : MonoBehaviour
{
    public static List<DialogTextRepository> dialogTextRepository=new List<DialogTextRepository>();
    public static Dictionary<string, float> ResultConfig_1 = new Dictionary<string, float>();
    public static Dictionary<string, float> ResultConfig_2 = new Dictionary<string, float>();
    public static Dictionary<string, float> ResultConfig_3 = new Dictionary<string, float>();
    
    private readonly List<string> _textPath = new List<string>(){
        "/DialogInfo/1-1Text.json",
        "/DialogInfo/1-2Text.json",
    };
    private readonly List<string> _dictionaryPath = new List<string>()
    {
        "/DialogInfo/ResultConfig_1.json"
    };

    private void Awake() 
    {
        foreach (var p in _textPath)
        {
            string data = ReadJsonUlilities.ReadJson(p);
            dialogTextRepository.Add(JsonConvert.DeserializeObject<DialogTextRepository>(data));
        }

        ResultConfig_1=JsonConvert.DeserializeObject<Dictionary<string, float>>(ReadJsonUlilities.ReadJson(_dictionaryPath[0]));
    }

    public static Dictionary<string, float> GetResultConfig(int configIndex)
    {
        if (configIndex==0)
            return ResultConfig_1;
        else if (configIndex == 1)
        {
            Debug.Log("this is config 2");
            return ResultConfig_2;
        }
        else
        {
            return ResultConfig_3;
        }
    }
}
