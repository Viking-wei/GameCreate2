using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton_2D<EnemyManager> 
{
    [SerializeField] bool spawnEnemy = true;
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] float timeBetweenSpawns = 1f;
    [SerializeField] float timeBetweenWaves = 1f;
    [SerializeField]int minEnemyAmount=3;
    [SerializeField]int maxEnemyAmount=10;
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
        waitTimeBetweenSpawns=new WaitForSeconds(timeBetweenSpawns);
        waitTimeBetweenWaves=new WaitForSeconds(timeBetweenWaves);
        waitUntilNoEnemy = new WaitUntil(()=>enemyList.Count==0);
    }
    
    IEnumerator Start()
    {
        while(spawnEnemy)
        {
            yield return waitUntilNoEnemy;
            yield return waitTimeBetweenWaves;
            yield return StartCoroutine(nameof(RandomlySpawnCoroutine));
        }
        
    }
    IEnumerator RandomlySpawnCoroutine()
    {
        enemyAmount = Mathf.Clamp(enemyAmount, minEnemyAmount + wavenumber/3, maxEnemyAmount);
        for(int i=0;i<enemyAmount;i++)
        {
            enemyList.Add(PoolManager.Release(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)]));

            yield return waitTimeBetweenSpawns;
        }
        wavenumber++;
        if(wavenumber>=7)
        {
            GameManager.Instance.ExitToNight();
        }
    }
    public void RemoveFromList(GameObject enemy)=>enemyList.Remove(enemy);
}
