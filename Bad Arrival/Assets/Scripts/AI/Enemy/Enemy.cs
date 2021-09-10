using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyObject enemyObject;

    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentShield;
    [SerializeField] private float maxShield;

    private EnemyAi gruntAi;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = enemyObject.currentHealth;
        maxHealth = enemyObject.maxHealth;
        currentShield = enemyObject.currentShield;
        maxShield = enemyObject.maxShield;

        gruntAi = this.gameObject.GetComponent<EnemyAi>();
    }

    public void ReceiveDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        gruntAi.StartAggro();
    }
}
