using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public class EventController : MonoBehaviour
{
    public UnityEvent enter1;
    public UnityEvent finish1;
    public UnityEvent enter2;
    public UnityEvent enter3;
    public UnityEvent enter4;

    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public GameObject whiteWalls;
    public Collider planecol;
    public GameObject plane;
    public GameObject[] spaces;
    public GameObject[] enemysIn1;
    public GameObject[] enemysIn2;
    public GameObject[] enemysIn3;
    public GameObject[] enemysIn4;
    bool isSpawn1=false;

    bool enter1happen=false;
    bool enter2happen=false;
    bool enter3happen=false;
    bool enter4happen=false;
   
    private void Start()
    {
        enter1.AddListener(Spawn1);
        enter2.AddListener(Spawn2);
        enter3.AddListener(Spawn3);
        enter4.AddListener(Spawn4);
        
        finish1.AddListener(DestroyWhite1);
        enemysIn1=new GameObject[4];
        enemysIn2 = new GameObject[2];
        enemysIn3 = new GameObject[3];
        enemysIn4 = new GameObject[8];
        
        
    }

    void Update()
    {
        if (spaces[0].gameObject.GetComponent<triggerspace>().isCollided == true&&enter1happen==false)
        {
            enter1happen = true;
            enter1.Invoke();
        }
        if (spaces[1].gameObject.GetComponent<triggerspace>().isCollided == true && enter2happen == false)
        {
            enter2happen = true;
            enter2.Invoke();
            
        }
        if (spaces[2].gameObject.GetComponent<triggerspace>().isCollided == true && enter3happen == false)
        {
            enter3happen = true;
            enter3.Invoke();

        }
        if (spaces[3].gameObject.GetComponent<triggerspace>().isCollided == true && enter4happen == false)
        { 
            enter4happen = true;
            enter4.Invoke();
        }
        if (isSpawn1)
        {
            bool allDestroyed = true;

            foreach (GameObject obj in enemysIn1)
            {
                if (obj != null)
                {
                    allDestroyed = false;

                }
            }

            if (allDestroyed)
            {
                finish1.Invoke();
            }
        }
    }
    private void Spawn1()
    {
       enemysIn1[0] = Instantiate(enemy1Prefab, new Vector3(-5, 0.3f, 5), Quaternion.identity);
       enemysIn1[1] = Instantiate(enemy1Prefab, new Vector3(5, 0.3f, 5), Quaternion.identity);
       enemysIn1[2] = Instantiate(enemy2Prefab, new Vector3(-5, 0.5f, 10), Quaternion.identity);
       enemysIn1[3] = Instantiate(enemy2Prefab, new Vector3(5, 0.5f, 10), Quaternion.identity);
       isSpawn1= true;
    }
    private void Spawn2() 
    {
        enemysIn2[0]= Instantiate(enemy1Prefab,new Vector3(-14,0.3f,5.5f),Quaternion.identity);
        enemysIn2[1] = Instantiate(enemy1Prefab, new Vector3(-11, 0.3f, 5.5f), Quaternion.identity);

    }
    private void Spawn3()
    {
        enemysIn3[0] = Instantiate(enemy1Prefab, new Vector3(-14, 0.3f, 14f), Quaternion.identity);
        enemysIn3[1] = Instantiate(enemy1Prefab, new Vector3(-12.5f, 0.3f, 14f), Quaternion.identity);
        enemysIn3[2] = Instantiate(enemy1Prefab, new Vector3(-11f, 0.3f, 14f), Quaternion.identity);
    }
    private void Spawn4()
    {
        enemysIn4[0] = Instantiate(enemy1Prefab, new Vector3(-22, 0.3f, 19), Quaternion.identity);
        enemysIn4[1] = Instantiate(enemy1Prefab, new Vector3(-22, 0.3f, 27), Quaternion.identity);
        enemysIn4[2] = Instantiate(enemy1Prefab, new Vector3(-3, 0.3f, 19), Quaternion.identity);
        enemysIn4[3] = Instantiate(enemy1Prefab, new Vector3(-3, 0.3f, 27), Quaternion.identity);
        enemysIn4[4] = Instantiate(enemy2Prefab, new Vector3(-22, 0.3f, 22), Quaternion.identity);
        enemysIn4[5] = Instantiate(enemy2Prefab, new Vector3(-3, 0.3f, 22), Quaternion.identity);
        enemysIn4[6] = Instantiate(enemy2Prefab, new Vector3(-22, 0.3f, 30), Quaternion.identity);
        enemysIn4[7] = Instantiate(enemy2Prefab, new Vector3(-3, 0.3f, 30), Quaternion.identity);
    }
    private void DestroyWhite1()
    {
        GameObject.Destroy(whiteWalls);
    }

}
