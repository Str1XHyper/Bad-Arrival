using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour
{
    private bool isActive = false;
    public List<GameObject> spawnPoints;
    public GameObject[] enemyTypes;
    private List<GameObject> enemiesList;
    private float spawnTimestamp;
    public float timeBetweenSpawns;
    private int spawnpointToUseIndex;
    private GameObject spawnpointToUse;
    public int maxAmountOfEnemies;


    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if(spawnTimestamp > 0)
            {
                if(Time.time - spawnTimestamp > timeBetweenSpawns)
                {
                    SpawnEnemy();
                }
            }
            else
            {
                SpawnEnemy();
            }
            
        }
    }

    public void SpawnEnemy()
    {
        spawnTimestamp = Time.time;
        spawnpointToUseIndex = Random.Range(1, (spawnPoints.Count - 1));
        spawnpointToUse = spawnPoints[spawnpointToUseIndex];
        GameObject enemy = Instantiate(enemyTypes[Random.Range(0, (enemyTypes.Length - 1))], spawnPoints[spawnpointToUseIndex].transform.position, Quaternion.identity);
        spawnPoints.RemoveAt(spawnpointToUseIndex);
        spawnPoints.Insert(0, spawnpointToUse);
        enemiesList.Add(enemy);
    }

    public void SetActive()
    {
        isActive = true;
    }

    public void SetInActive()
    {
        isActive = false;
    }
}
