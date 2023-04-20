using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="NewDialog",menuName ="Dialog")]
public class DialogStorage : ScriptableObject
{
    public string theNpcName;
    public List<DialogContent> thePara;
}

[Serializable]
public class DialogContent
{
    public int ID;
    public bool haveBranch;
    public bool isPlayer;
    [Tooltip("增加或是减少多少好感，默认不增减")]
    public int FavoriabilityChange;
    [Required]public List<Branch> branches;
    
}

[Serializable]
public class Branch
{
    public string dialog;
    public int jumpID;
}

