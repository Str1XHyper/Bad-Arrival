using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAggroTrigger : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            enemy.GetComponent<EnemyAi>().StartAggro();
        }
        
    }
}
