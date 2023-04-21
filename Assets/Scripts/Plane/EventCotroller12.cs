using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventCotroller12 : MonoBehaviour
{
    public UnityEvent enter1;
    public UnityEvent enter2;
    public UnityEvent enter3;
    public UnityEvent enter4;
    public UnityEvent finish;


    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public GameObject outside;


    public Collider planecol;
    public GameObject plane;
    public GameObject[] spaces;
    public GameObject[] enemysIn1;
    public GameObject[] enemysIn2;
    public GameObject[] enemysIn3;
    public GameObject[] enemysIn4;



    bool enter1happen = false;
    bool enter2happen = false;
    bool enter3happen = false;
    bool enter4happen = false;
    bool isSpawnAll = false;

    private void Start()
    {
        enter1.AddListener(Spawn1);
        enter2.AddListener(Spawn2);
        enter3.AddListener(Spawn3);
        enter4.AddListener(Spawn4);
        finish.AddListener(Isalldown);



        enemysIn1 = new GameObject[2];
        enemysIn2 = new GameObject[3];
        enemysIn3 = new GameObject[1];
        enemysIn4 = new GameObject[2];
        



    }

    void Update()
    {
        if (spaces[0].gameObject.GetComponent<triggerspace>().isCollided == true && enter1happen == false)
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
        if (isSpawnAll)
        {
            bool allDestroyed = true;

            foreach (GameObject obj in enemysIn4)
            {
                if (obj != null)
                {
                    allDestroyed = false;

                }
            }

            if (allDestroyed)
            {
                finish.Invoke();
            }
        }

    }
    private void Spawn1()
    {
        enemysIn1[0] = Instantiate(enemy1Prefab, new Vector3(-4, 0.3f, 15), Quaternion.identity);
        enemysIn1[1] = Instantiate(enemy1Prefab, new Vector3(-10, 0.3f, 15), Quaternion.identity);
    }
    private void Spawn2()
    {
        enemysIn2[0] = Instantiate(enemy1Prefab, new Vector3(-8.5f, 0.3f, 23), Quaternion.identity);
        enemysIn2[1] = Instantiate(enemy1Prefab, new Vector3(-13, 0.3f, 23), Quaternion.identity);
        enemysIn2[2] = Instantiate(enemy2Prefab, new Vector3(-4, 0.3f, 22), Quaternion.identity);
    }
    private void Spawn3()
    {
        enemysIn3[0] = Instantiate(enemy2Prefab, new Vector3(10, 0.3f, 23.5f), Quaternion.identity);
        
    }
    private void Spawn4()
    {
        enemysIn4[0] = Instantiate(enemy1Prefab, new Vector3(18, 0.3f, 23), Quaternion.identity);
        enemysIn4[1] = Instantiate(enemy1Prefab, new Vector3(21, 0.3f, 23), Quaternion.identity);
        
        isSpawnAll = true;
    }
    private void Isalldown()
    {
        Destroy(outside);
    }

}