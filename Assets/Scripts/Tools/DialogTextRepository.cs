using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogTextRepository
{
    public List<DialogTextRepositoryNode> Data;
}

[Serializable]
public class DialogTextRepositoryNode
{
    public int ID;
    public string Name;
    public string Content;
    public int JumpID;
    public int ExtendInfo;
}