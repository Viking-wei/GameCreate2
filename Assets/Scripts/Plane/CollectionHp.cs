using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionHp : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            col.GetComponentInParent<Plane>().CollectionHp+=1;
            Destroy(this.gameObject);
        }
    }
}
