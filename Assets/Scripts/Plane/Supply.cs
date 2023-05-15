using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supply : MonoBehaviour
{

    int health = 1;
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag=="Player")
        {
            col.GetComponentInParent<Plane>().TakeHealth(health);
            Destroy(this.gameObject);
        }
    }
}
