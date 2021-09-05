using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] private Transform order;
    private Transform orderTemp;
    private Transform returnOrder;
    [SerializeField] private GameObject player;
    private AiBehaviourState BehaviourState;
    [SerializeField] private float moveSpeed;

    private NavMeshAgent agent;

    public enum AiBehaviourState
    {
        Idle, 
        Patrol,
        Aggro,
        Attack,
        Return
    }

    // Start is called before the first frame update
    void Start()
    {
        BehaviourState = AiBehaviourState.Idle;
        agent = GetComponent<NavMeshAgent>();
        agent.destination = order.position;
        agent.speed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        switch (BehaviourState)
        {
            case AiBehaviourState.Idle:
                Idle();
                break;
            case AiBehaviourState.Patrol:
                Patrol();
                break;
            case AiBehaviourState.Aggro:
                Aggro();
                break;
            case AiBehaviourState.Attack:
                Attack();
                break;
            case AiBehaviourState.Return:
                Return();
                break;
            default:
                break;
        }
    }

    private void Idle()
    {
        
    }

    private void Patrol()
    {
        
    }

    private void Aggro()
    {
        agent.destination = order.position;
    }

    private void Attack()
    {

    }

    private void Return()
    {
        
        Debug.Log("Returning");
    }

    public void StartAggro()
    {
        if( orderTemp == null)
        {
            orderTemp = order;
        }
        order = player.transform;
        returnOrder = transform;
        Debug.Log("Start Aggro");
        BehaviourState = AiBehaviourState.Aggro;
    }

    public void StopAggro()
    {
        order = returnOrder;
        agent.destination = order.position;
        Debug.Log("Stop Aggro");
        BehaviourState = AiBehaviourState.Return;
    }
}
