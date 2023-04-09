using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackCube : MonoBehaviour
{
    // Start is called before the first frame update
    public int hp = 5;
    
    public void TakeDamage(int damage)
    {
        if (hp <= 0) return;
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        };
    }
}
