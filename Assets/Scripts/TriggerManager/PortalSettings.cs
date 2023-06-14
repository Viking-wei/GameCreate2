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
    [Header("Lock")] 
    public bool isLocked;
    
    private PortalPair _portalPair;
    
    private void Awake()
    {
        if(portal1!=null&&portal2!=null) 
            _portalPair = new PortalPair(portal1, portal2);
    }
    /// <summary>
    /// try to get the target position of the portal,if the portal is locked, return false
    /// </summary>
    /// <param name="portal">the door entered</param>
    /// <param name="targetPosition">the position door pointed</param>
    /// <returns></returns>
    public bool TryGetTargetPosition(GameObject portal,out Vector3 targetPosition)
    {
        targetPosition = _portalPair.GetTarget(portal);
        return !isLocked;
    }

}
