using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public float speed = 20;
    //private Transform target;
    public bool isEnemyBullet=false;
    public bool isSlow = false;


    public GameObject explosionEffectPrefab;

    //public void SetTarget(Transform target)
    //{ this.target = target; }

 
    private void Update()
    {

        if (isEnemyBullet == false)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else if(isEnemyBullet&&isSlow)
        {
            transform.Translate(Vector3.forward*(speed*0.05f)*Time.deltaTime);
        }
        else
        { transform.Translate(Vector3.forward*(speed*0.25f)*Time.deltaTime);}

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy" && isEnemyBullet == false)
        {
            col.GetComponent<Enemy>().TakeDamage(damage);
            Die();
        }
       
        else if (col.tag == "core" && isEnemyBullet == false)
        {
            col.GetComponent<core>().TakeDamage(damage);
            Die();
        }
        else if(col.tag =="BlackCube")
        {
            if (isEnemyBullet == false)
            {
                col.GetComponent<BlackCube>().TakeDamage(damage);
            }
            Die();
        }
        else if(col.tag=="Player"&&isEnemyBullet==true)
        {
            col.GetComponentInParent<Plane>().TakeDamage(damage);
            Die();
        }
        else if(col.tag=="Bullet"&&isEnemyBullet==false
            &&col.GetComponent<Bullet>().isEnemyBullet==true&&col.GetComponent<Bullet>().isSlow==false)
        {
            Die();
        }
        else if(col.tag=="Bullet"&&isEnemyBullet==true&&isSlow==false&&col.GetComponent<Bullet>().isEnemyBullet==false)
        {
            Die();
        }
        else if(col.tag =="Wall")
        {
            Die();
        }


    }

    void Die()
    {
        GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        Destroy(effect, 1);
        Destroy(this.gameObject);
    }
}
