using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class core : MonoBehaviour
{
    public int hp = 3;
    
    public void TakeDamage(int damage)
    {
        if (hp <= 0) return;
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(this.gameObject);
            GameManager.Instance.ExitToNight();
        };
    }
}
