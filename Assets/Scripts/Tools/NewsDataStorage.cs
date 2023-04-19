using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Newsnode
{
    public int NewsID;
    public string NewsTittle;
    public string NewsBody;
}

[Serializable]
public class NewsList
{
    public List<Newsnode> NewsData;
}
