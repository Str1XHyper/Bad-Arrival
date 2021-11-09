using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyObject enemyObject;

    private float currentHealth;
    private float maxHealth;
    private float currentShield;
    private float maxShield;

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
        currentHealth -= damageAmount;
        if(currentHealth <= 0)
        {
            var item = enemyObject.lootpool.GenerateItem();
            if(item != null)
            {
                GameObject droppedItem = Instantiate(item.Model, transform.position, Quaternion.identity);
                droppedItem.GetComponent<GroundItem>().item = item;
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
