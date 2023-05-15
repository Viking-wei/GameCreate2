using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public static int damage = 3;
    public float speed = 20;
    //private Transform target;
    public bool isEnemyBullet=false;
    public bool isSlow = false;
    public static int countAttack = 0;
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
        AddDamage();

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
            col.GetComponentInParent<Plane>().TakeDamage(1);
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
        //GameObject effect = ObjectPool.Instance.GetObject(explosionEffectPrefab);
        //effect.transform.position=this.transform.position;
        //effect.transform.rotation=this.transform.rotation;
        ObjectPool.Instance.PushObject(this.gameObject);
        //Destroy(effect,1);   
    }
   
    public void AddDamage()
    {
        if(countAttack==3)
        {
            countAttack = 0;
            damage++;
        }
    }
}
