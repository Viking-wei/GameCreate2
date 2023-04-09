using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerspace : MonoBehaviour
{
    public bool isCollided=false;
    private void OnTriggerEnter(Collider col)
    {
        if (!isCollided&&col.tag=="Player")
        {
            isCollided = true;
        }
    }
}
