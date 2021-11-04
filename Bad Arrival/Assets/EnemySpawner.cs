using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyToSpawn;
    public int SpawnTimerInSeconds;

    private float SpawnTimerInFrames;

    private void Start()
    {
        SpawnTimerInFrames = SpawnTimerInSeconds / Time.fixedDeltaTime;
    }
    private void FixedUpdate()
    {
        SpawnTimerInFrames--;
        if(SpawnTimerInFrames <= 0)
        {
            var obj = Instantiate(EnemyToSpawn, transform.position + new Vector3(0, 5, 0), Quaternion.identity);
            SpawnTimerInFrames = SpawnTimerInSeconds / Time.fixedDeltaTime;
        }
    }
}
