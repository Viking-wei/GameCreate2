using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="NewDialog",menuName ="Dialog")]
public class DialogStorage : ScriptableObject
{
    public List<DialogContent> thePara;
}

[Serializable]
public class DialogContent
{
    [Required]public bool isNpc;
    public string theNpcName;
    [Required]public string dialog;
    [Required]public bool isEndSentence;
}