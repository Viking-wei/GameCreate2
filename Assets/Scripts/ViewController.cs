using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    public Transform PlaneTransform;
    public float speed=1;

    private void Update()
    {
        if (PlaneTransform != null)
        {
            Vector3 targetposition = new Vector3(PlaneTransform.position.x, transform.position.y, PlaneTransform.position.z);
            transform.Translate((targetposition - transform.position) * Time.deltaTime * speed, Space.World);
        }
    }

}
   
