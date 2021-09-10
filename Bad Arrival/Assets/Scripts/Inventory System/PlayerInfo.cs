using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [Header("Info")]
    [SerializeField] private TMP_Text healthText;


    public void UpdateHealth(int currentHP)
    {
        int currentHealth = Mathf.RoundToInt((float) currentHP / (float)Player.instance.MaxHP * 100);
        healthText.text = $": {currentHealth}/100";
    }
}
