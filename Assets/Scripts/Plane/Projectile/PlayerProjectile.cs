using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile
{
    TrailRenderer trail;
   
    private void Awake()
    {
        //float angle = player.transform.rotation.z;
        //Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

        trail=GetComponentInChildren<TrailRenderer>();
        //if (moveDirection != direction)
        //{
        //    transform.GetChild(0).rotation = Quaternion.FromToRotation(direction,moveDirection);
        //}
    }
    
    private void OnDisable()
    {
        trail.Clear();
    }
}
