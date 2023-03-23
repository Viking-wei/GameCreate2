using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalDifferInfo : MonoBehaviour
{
    public DifferInfo differInfo;
}

[Serializable] 
public class DifferInfo
{
    public int targetSceneIndex;
    //remember y-axis need to add character's height
    public Vector3 targetDestination;
}
