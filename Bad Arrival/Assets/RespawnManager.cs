using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{

    #region Singleton
    public static RespawnManager instance;

    private void Awake()
    {
        if (instance == null || instance != this)
        {
            instance = this;
        }
    }
    #endregion

    [SerializeField] private Transform RespawnLocation;

    private void FixedUpdate()
    {
        if (Player.instance.GetHealth() <= 0)
        {
            RespawnPlayer();
        }
    }

    void RespawnPlayer()
    {
        var player = Player.instance.gameObject;
        Player.instance.ResetHealth();
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = RespawnLocation.position;
        player.GetComponent<CharacterController>().enabled = true;
    }
}
