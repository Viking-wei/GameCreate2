using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMap1 : MonoBehaviour
{
    GameObject mapSprite;

    private void Awake()
    {
        mapSprite=transform.parent.gameObject;

        mapSprite.GetComponent<SpriteRenderer>().enabled=false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            mapSprite.GetComponent<SpriteRenderer>().enabled=true;
        }
    }
}
