using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventController11 : MonoBehaviour
{ 
    public UnityEvent enter1;
    
    public UnityEvent enter2;
    public UnityEvent finish;
    

    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public GameObject outside;
    
    
    public Collider planecol;
    public GameObject plane;
    public GameObject[] spaces;
    public GameObject[] enemysIn1;
    public GameObject[] enemysIn2;
  
    

    bool enter1happen = false;
    bool enter2happen = false;
    bool isSpawnAll = false;

    private void Start()
    {
        enter1.AddListener(Spawn1);
        enter2.AddListener(Spawn2);
        finish.AddListener(Isalldown);
       

        
        enemysIn1 = new GameObject[2];
        enemysIn2 = new GameObject[1];
        


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
        if(isSpawnAll)
        {
            bool allDestroyed = true;

            foreach (GameObject obj in enemysIn2)
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
        enemysIn1[0] = Instantiate(enemy1Prefab, new Vector3(-19, 0.3f,19), Quaternion.identity);
        enemysIn1[1] = Instantiate(enemy1Prefab, new Vector3(-20, 0.3f,16), Quaternion.identity);
        
       
    }
    private void Spawn2()
    {
        enemysIn2[0] = Instantiate(enemy2Prefab, new Vector3(1, 0.3f, 35), Quaternion.identity);
       isSpawnAll = true;
    }  
    private void Isalldown()
    {
        Destroy(outside);
    }

}
