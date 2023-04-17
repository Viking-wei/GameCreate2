using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="NewDialog",menuName ="Dialog")]
public class DialogStorage : ScriptableObject
{
    public int ID;
    public string theNpcName;
    public List<DialogContent> thePara;


}

[Serializable]
public class DialogContent
{
    public bool haveBranch;
    public bool isPlayer;
    [Required]public List<Branch> branches;

}

[Serializable]
public class Branch
{
    public string dialog;
    public int jumpID;
}