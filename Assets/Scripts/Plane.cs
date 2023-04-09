using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Plane : MonoBehaviour
{
    public float speed=1;
    public int hp = 3;
    float timer = 0;
    public GameObject bulletPrefab;
    public Transform firePosition;
    float attackRateTime = 0.3f;
    public List<GameObject> enemys = new List<GameObject>();
    

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            enemys.Add(col.gameObject);
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Enemy")
        {
            enemys.Remove(col.gameObject);
        }
    }

  
    void Update()
    {
        GameObject.Find("Texthp").GetComponent<Text>().text = "Hp=" + hp;
        Move();
        Rotate();
        timer += Time.deltaTime;
       
        if (timer >= attackRateTime&&Input.GetKey(KeyCode.A))
        {
            timer = 0;
            Attack();  
        }
        
    }
    private void FixedUpdate()
    {
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    void Move()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * speed, Space.World);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        { 
            transform.Translate(new Vector3(0, 0,-1) * Time.deltaTime * speed, Space.World);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        { 
            transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * speed, Space.World); 
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime * speed, Space.World);
        }
    }
    void Rotate()
    {
        if (Input.GetKey(KeyCode.Q)&&enemys.Count > 0 && enemys[0] != null)
        {
                transform.LookAt(enemys[0].transform);
        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow))
            {
                transform.localRotation = Quaternion.Euler(0, 45, 0);
            }
            else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow))
            {
                transform.localRotation = Quaternion.Euler(0, -45, 0);
            }
            else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow))
            {
                transform.localRotation = Quaternion.Euler(0, 135, 0);
            }
            else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow))
            {
                transform.localRotation = Quaternion.Euler(0, -135, 0);
            }
            else
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    transform.localRotation = Quaternion.Euler(0, 180, 0);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    transform.localRotation = Quaternion.Euler(0, 90, 0);
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.localRotation = Quaternion.Euler(0, -90, 0);
                }
            }
        }
    }

    private void Attack()
    {
        if (enemys.Count>0&&enemys[0] == null)
            {
                UpdateEnemys();
            }
       
      GameObject bullet = GameObject.Instantiate(bulletPrefab, firePosition.position, Quaternion.identity);
      bullet.transform.forward = transform.forward;
       
    }
    public void TakeDamage(int damage)
    {
        
        hp -= damage;
        if (hp <= 0)
        {
            GameObject.Find("Texthp").GetComponent<Text>().text = "Hp=" + 0;
            Die();
        };
    }
    void UpdateEnemys()
    {
        enemys = enemys.FindAll(x => x != null);
    }
    void Die()
    {
       Destroy(gameObject);
    }
}
