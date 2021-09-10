using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Grunt", menuName = "Enemies/Grunt")]
public class EnemyObject : ScriptableObject
{
    public float aggroMoveSpeed;
    public float patrolMoveSpeed;
    public float attackRange;
    public float timeBetweenAttacks;

    public float maxHealth;
    public float currentHealth;
    public float maxShield;
    public float currentShield;

    public GameObject[] abilities;
}
