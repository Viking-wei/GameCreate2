using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;
public class PortalSettings : MonoBehaviour
{
    [Header("Portal Setting")]
    public GameObject portal1;
    public GameObject portal2;
    
    private PortalPair _portalPair;

    private void Awake()
    {
        if(portal1!=null&&portal2!=null) 
            _portalPair = new PortalPair(portal1, portal2);
    }

    public Vector3 GetTargetPosition(GameObject portal)
    {
        return _portalPair.GetTarget(portal);
    }
    
}
