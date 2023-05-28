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
                
                int number = Random.Range(stepToStart+1, (stepToStart*6/5)+1);
                for (int i = 0; i < number; i++)
                {
                    PoolManager.Release(enemyPrefab1, RandomPointInBounds(GetComponent<Collider2D>().bounds));
                }
            }
            isTriggered = true;
        }
    }
}
