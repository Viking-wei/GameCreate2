using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    [Header("----MOVE----")]
    [SerializeField] float moveSpeed = 2f;
    [SerializeField]float radius=30f;
    Transform playerTransform;
    [Header("----FIRE----")]
    [SerializeField] GameObject[] projectiles;
    [SerializeField] Transform muzzle;
    [SerializeField] float minFireInterval;
    [SerializeField] float maxFireInterval;
    
    public void Start()
    {
        playerTransform=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void Update()
    {
        if(playerTransform!=null)
        {
            float distance =(transform.position - playerTransform.position).sqrMagnitude;
            if(distance < radius&&distance>4f)
            {
                transform.position=Vector2.MoveTowards(transform.position,playerTransform.position,
                    moveSpeed*Time.deltaTime);
                
            }
            else if (distance <= 4f && distance > 0f)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position,
                    (moveSpeed/2) * Time.deltaTime);
                
            }
           
            
            if((transform.position.x-playerTransform.position.x)>0)
            {
                transform.GetChild(0).localScale = new Vector3(-1,1,1);
            }
            else
            {
                transform.GetChild(0).localScale=new Vector3(1, 1, 1);
            }
        }
    }
    private void OnEnable()
    {
        StartCoroutine(nameof(RandomlyFireCoroutine));
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

       IEnumerator RandomlyFireCoroutine()
        {

            while (gameObject.activeSelf)
            {
                yield return new WaitForSeconds(Random.Range(minFireInterval, maxFireInterval));

                foreach (var projectile in projectiles)
                {
                    PoolManager.Release(projectile, muzzle.position);
                }
            }
        }
   
    }

