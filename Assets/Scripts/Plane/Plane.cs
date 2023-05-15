using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Plane : MonoBehaviour
{
    public float speed=1;
    public int hp = 7;
    public int limithp = 7;
    public int CollectionHp = 0;
    float timer = 0;
    public GameObject bulletPrefab;
    public Transform firePosition;
    float attackRateTime = 0.3f;
    public static int countATK=0;
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
        GameObject.Find("TextATK").GetComponent<Text>().text = "¹¥»÷Á¦=" +(3+(countATK/3));
        Move();
        Rotate();
        AddHealth();
        timer += Time.deltaTime;
       
        if (timer >= attackRateTime&&Input.GetKey(KeyCode.J))
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
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * speed, Space.World);
        }
        if (Input.GetKey(KeyCode.S))
        { 
            transform.Translate(new Vector3(0, 0,-1) * Time.deltaTime * speed, Space.World);
        }
        if (Input.GetKey(KeyCode.D))
        { 
            transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * speed, Space.World); 
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime * speed, Space.World);
        }
    }
    void Rotate()
    {
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (enemys.Count > 0 && enemys[0] != null)
            {
                Vector3 targetPosition = enemys[0].transform.position;
                targetPosition.y = transform.position.y;
                transform.LookAt(targetPosition);
                transform.Rotate(new Vector3(-90, 0, 0), Space.Self);
            }
            
        }
        else
        {
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
            {
                transform.localRotation = Quaternion.Euler(-90, 45, 0);
            }
            else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            {
                transform.localRotation = Quaternion.Euler(-90, -45, 0);
            }
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
            {
                transform.localRotation = Quaternion.Euler(-90, 135, 0);
            }
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
            {
                transform.localRotation = Quaternion.Euler(-90, -135, 0);
            }
            else
            {
                if (Input.GetKey(KeyCode.W))
                {
                    transform.localRotation = Quaternion.Euler(-90, 0, 0);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    transform.localRotation = Quaternion.Euler(-90, 180, 0);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    transform.localRotation = Quaternion.Euler(-90, 90, 0);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    transform.localRotation = Quaternion.Euler(-90, -90, 0);
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

        //GameObject bullet = GameObject.Instantiate(bulletPrefab, firePosition.position, Quaternion.identity);
        GameObject bullet = ObjectPool.Instance.GetObject(bulletPrefab);
        bullet.transform.position = firePosition.position;
        bullet.transform.rotation=Quaternion.identity;
      bullet.transform.forward = -transform.up;
       
    }
    public void AddHealth()
    {
        if (CollectionHp == 3)
        {
            CollectionHp = 0;
            limithp +=2;
            hp +=2;
        }
    }
    public void TakeDamage(int damage)
    {
        
        hp -= damage;
        BlinkPlayer(2, 0.1f);
        if (hp <= 0)
        {
            GameObject.Find("Texthp").GetComponent<Text>().text = "Hp=" + 0;
            Die();
        };
    }
    void BlinkPlayer(int numBlinks, float seconds)
    {
        StartCoroutine(DoBlinks(numBlinks, seconds));
    }
    IEnumerator DoBlinks(int numBlinks, float seconds)
    {
        for (int i = 0; i < numBlinks * 2; i++)
        {
            GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(seconds);
            GetComponent<Renderer>().enabled = true;
            yield return new WaitForSeconds(seconds);
        }
        
    }

    public void TakeHealth(int health) 
    {
        if(hp>0&&hp<limithp)
        hp += health;
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
