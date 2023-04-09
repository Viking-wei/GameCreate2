using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 0.3f;
    public float distance = 5;
    public int hp = 5;
    private float timer;
    public bool isEnemy1 = false;
    public bool isEnemy2 = false;
    public GameObject enemyBulletPrefab1;
    public GameObject enemyBulletPrefab2;
    public float attackRateTime = 1;
    Transform plane;

    private void Start()
    {
        GameObject Logo = GameObject.Find("Logo");
        plane = Logo.transform;
    }



    private void Update()
    {
        if (plane != null)
        {
            Move();
            timer += Time.deltaTime;

            if (timer >= attackRateTime && Vector3.Distance(transform.position, plane.transform.position) < (2 * distance))
            {
                timer = 0;
                Attack();
            }
        }
    }




    private void FixedUpdate()
    {
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    public void TakeDamage(int damage)
    {
        if (hp <= 0) return;
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        };
    }
    void Move()
    {


        if (isEnemy1)
        {
            transform.LookAt(new Vector3(plane.transform.position.x, this.transform.position.y, plane.transform.position.z)); 
            if (Vector3.Distance(transform.position, plane.transform.position) > distance)
            {
                transform.Translate(transform.forward * Time.deltaTime * speed, Space.World);
            }
            else
            {
                transform.Translate(transform.forward * Time.deltaTime * (speed / 5f), Space.World);
            }
        }
        if (isEnemy2)
        {
            transform.Rotate(0, Time.deltaTime * 25, 0);
        }
    }
    void Attack()
    {
        if (isEnemy1)
        {
            GameObject bullet = GameObject.Instantiate(enemyBulletPrefab1, transform.position, Quaternion.identity);
            bullet.transform.forward = transform.forward;
        }
        if (isEnemy2)
        {
            GameObject bullet1 = GameObject.Instantiate(enemyBulletPrefab2, transform.position, Quaternion.identity);
            bullet1.transform.forward = transform.forward;
            GameObject bullet2 = GameObject.Instantiate(enemyBulletPrefab2, transform.position, Quaternion.identity);
            bullet2.transform.forward = -transform.forward;
            GameObject bullet3 = GameObject.Instantiate(enemyBulletPrefab2, transform.position, Quaternion.identity);
            bullet3.transform.forward = transform.right;
            GameObject bullet4 = GameObject.Instantiate(enemyBulletPrefab2, transform.position, Quaternion.identity);
            bullet4.transform.forward = -transform.right;
        }
    }
}
