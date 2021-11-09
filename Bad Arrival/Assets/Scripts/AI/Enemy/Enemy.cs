using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public EnemyObject enemyObject;

    private float currentHealth;
    private float maxHealth;
    private float currentShield;
    private float maxShield;
    private bool aggroed = false;
    [SerializeField] private UnityEvent startEvent;
    [SerializeField] private UnityEvent deathEvent;

    private EnemyAi enemyAi;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = enemyObject.currentHealth;
        maxHealth = enemyObject.maxHealth;
        currentShield = enemyObject.currentShield;
        maxShield = enemyObject.maxShield;

        enemyAi = this.gameObject.GetComponent<EnemyAi>();
    }

    public void ApplyDamage(int damageAmount)
    {
        if (!aggroed)
        {
            startEvent.Invoke();
        }

        currentHealth -= damageAmount;
        if(currentHealth <= 0)
        {
            var item = enemyObject.lootpool.GenerateItem();
            if(item != null)
            {
                GameObject droppedItem = Instantiate(item.Model, transform.position, Quaternion.identity);
                droppedItem.GetComponent<GroundItem>().item = item;
            }
            if(deathEvent != null)
            {
                deathEvent.Invoke();
            }
            Destroy(gameObject);
        }
        enemyAi.StartAggro();
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float getMaxHealth()
    {
        return maxHealth;
    }
}
