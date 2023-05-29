using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

namespace UI
{
    public class DialogText : MonoBehaviour
{
    public static List<DialogTextRepository> dialogTextRepository=new List<DialogTextRepository>();
    private static Dictionary<string, float> ResultConfig_1 = new Dictionary<string, float>();
    private static Dictionary<string, float> ResultConfig_2 = new Dictionary<string, float>();
    private static Dictionary<string, float> ResultConfig_3 = new Dictionary<string, float>();

    
    private readonly List<string> _textPath = new List<string>(){
        "/DialogInfo/1-1Text.json",
        "/DialogInfo/1-2Text.json",
        "/DialogInfo/1-3Text.json",
        "/DialogInfo/1-4Text.json",
        "/DialogInfo/2-1Text.json",
        "/DialogInfo/2-2Text.json",
        "/DialogInfo/2-3Text.json",
        "/DialogInfo/2-4Text.json",
        "/DialogInfo/2-5Text.json",
        "/DialogInfo/2-6Text.json"
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
        // foreach (var item in ResultConfig_1)
        // {
        //     Debug.Log(item.Key+" "+item.Value);
        // }
    }

    public static Dictionary<string, float> GetResultConfig(int configIndex)
    {
        if (configIndex==1)
            return ResultConfig_1;
        else if (configIndex == 2)
        {
            Debug.Log("this is config 2");
            return ResultConfig_2;
        }
        else
        {
            Debug.Log("this is config 3");
            return ResultConfig_3;
        }
    }
    public static int GetAnswerIndex(string answer,int configIndex)
    {
        if(configIndex==1)
        {
            if(answer=="Good")
                return 0;
            else if(answer=="Normal")
                return 4;
            else if(answer=="Bad")
                return 8;
            else
            {
                Debug.LogError("Error answer");
                return -1;
            }
        }
        else if(configIndex==2)
        {
            //TODO: paragraph 2
                Debug.LogError("answer is not A B C D");
                return -1;
        }
        else
        {
            //TODO: paragraph 3
            Debug.LogError("Have not config this paragraph");
            return -1;
        }
    }
}
}

