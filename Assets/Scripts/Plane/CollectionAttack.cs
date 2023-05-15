using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionAttack : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Bullet.countAttack++;
            Plane.countATK++;
            Destroy(this.gameObject);
        }
    }
}
