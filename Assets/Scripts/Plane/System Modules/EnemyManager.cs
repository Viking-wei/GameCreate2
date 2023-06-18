using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton_2D<EnemyManager> 
{
    [SerializeField] GameObject[] enemyPrefabs;

    int enemyAmount;
    List<GameObject> enemyList;
    WaitForSeconds waitTimeBetweenSpawns;
    WaitForSeconds waitTimeBetweenWaves;
    WaitUntil waitUntilNoEnemy;
    protected override void Awake()
    {
        base.Awake();
        enemyList = new List<GameObject>();
       
        waitUntilNoEnemy = new WaitUntil(()=>enemyList.Count==0);
    }
    
    public void RemoveFromList(GameObject enemy)=>enemyList.Remove(enemy);
}
