using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;


public enum HackerProperty
{
    None,
    Unlocked,
    End
};

public class HackerPropertyController : MonoBehaviour
{
    public HackerProperty hackerProperty;
    public int targetPlaneIndex;
    public List<PortalSettings> thePortalList;

    public void UnLockedAllTriggeredPortals()
    {
        if (thePortalList.Count>0)
        {
            foreach (var portal in thePortalList)
                portal.isLocked = false;
            
            thePortalList.Clear();
        }
        else
        {
            Debug.Log("No portal to unlock");
        }
    }
}
