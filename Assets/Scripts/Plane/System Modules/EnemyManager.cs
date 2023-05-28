using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton_2D<EnemyManager> 
{
    //[SerializeField] bool spawnEnemy = true;
    [SerializeField] GameObject[] enemyPrefabs;
    
    //[SerializeField]int minEnemyAmount=3;
    //[SerializeField]int maxEnemyAmount=10;
    int wavenumber = 1;
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
