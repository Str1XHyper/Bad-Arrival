using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHandler : MonoBehaviour
{
    private Enemy enemy;
    public GameObject explosionAbility;


    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.GetCurrentHealth() < (enemy.getMaxHealth() / 2) && explosionAbility != null)
        {
            explosionAbility.SetActive(true);
        }
    }
}
