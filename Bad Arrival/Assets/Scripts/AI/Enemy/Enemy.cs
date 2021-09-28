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
        if(gruntAi != null)
        {
            gruntAi.StartAggro();
        }
        
    }
}
