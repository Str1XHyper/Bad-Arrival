using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAggroTrigger : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemy.GetComponent<EnemyAi>().StopAggro();
        }
    }
}
