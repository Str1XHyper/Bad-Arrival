using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float mana;
    [SerializeField] private float shield;

    private EnemyAi gruntAi;

    // Start is called before the first frame update
    void Start()
    {
        gruntAi = this.gameObject.GetComponent<EnemyAi>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceiveDamage()
    {
        gruntAi.StartAggro();
    }
}
