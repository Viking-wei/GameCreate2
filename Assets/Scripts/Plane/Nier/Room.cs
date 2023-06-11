using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject doorLeft,doorRight,doorUp,doorDown;

    public bool roomLeft,roomRight,roomUp,roomDown;

    public int stepToStart;
    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    public GameObject enemyPrefab3;
    public int doorNumber;
    bool isTriggered=false;
    

    void Start()
    {
        doorLeft.SetActive(roomLeft);
        doorRight.SetActive(roomRight);
        doorUp.SetActive(roomUp);
        doorDown.SetActive(roomDown);
    }

    // Update is called once per frame
    public void UpdateRoom(float xOffset,float yOffset)
    {
        stepToStart = (int)(Mathf.Abs(transform.position.x / xOffset) + Mathf.Abs(transform.position.y / yOffset));
        if (roomUp)
            doorNumber++;
        if(roomDown)
            doorNumber++;
        if (roomLeft)
            doorNumber++;
        if(roomRight)
            doorNumber++;
    }

    public Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            
            
            CameraController.instance.ChangeTarget(transform);
            this.GetComponent<SpriteRenderer>().enabled = true;
            stepToStart = (int)(Mathf.Abs(transform.position.x / 18) + Mathf.Abs(transform.position.y / 10));
            if (!isTriggered&&stepToStart>0)
            {
                
                int number1 = Random.Range(stepToStart+1, (stepToStart*3/2)+1);
                int number2 = Random.Range((stepToStart * 11 / 12) -1, (stepToStart * 11 / 9) );
                int number3 = Random.Range((stepToStart*11/19) - 2, (stepToStart * 11 / 13) - 1);
                for (int i = 0; i < number1; i++)
                {
                    PoolManager.Release(enemyPrefab1, RandomPointInBounds(GetComponent<Collider2D>().bounds));
                }
                for (int i = 0; i < number2; i++)
                {
                    PoolManager.Release(enemyPrefab2, RandomPointInBounds(GetComponent<Collider2D>().bounds));
                }
                for (int i = 0; i < number3; i++)
                {
                    PoolManager.Release(enemyPrefab3, RandomPointInBounds(GetComponent<Collider2D>().bounds));
                }
            }
            isTriggered = true;
        }
    }
}
