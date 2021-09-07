using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] private Transform order;
    [SerializeField] private GameObject patrolGroup;
    private int currentPatrolStage = 0;
    [SerializeField] private Transform orderTemp;
    [SerializeField] private GameObject player;

    [SerializeField] private AiBehaviourState BehaviourState;
    [SerializeField] private float aggroMoveSpeed;
    [SerializeField] private float patrolMoveSpeed;

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
        //agent.destination = order.position;
        agent.speed = patrolMoveSpeed;
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
        if(patrolGroup != null)
        {
            BehaviourState = AiBehaviourState.Patrol;
        }
    }

    private void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            if(patrolGroup != null)
            {
                GoToNextPatrolOrder();
            }
            

        }
            
    }

    private void Aggro()
    {
        OrderPeriodically();
    }

    private void Attack()
    {

    }

    private void Return()
    {
        OrderPeriodically();
        Debug.Log("Returning");
    }

    public void StartAggro()
    {
        if( orderTemp == null)
        {
            orderTemp = order;
        }
        order = player.transform;
        Debug.Log("Start Aggro");
        BehaviourState = AiBehaviourState.Aggro;
        agent.speed = aggroMoveSpeed;
    }

    public void StopAggro()
    {
        order = orderTemp;
        agent.destination = order.position;
        Debug.Log("Stop Aggro");
        BehaviourState = AiBehaviourState.Return;
        agent.speed = patrolMoveSpeed;
    }

    private float timeBetweenOrders = 0.1f;
    private float timeSincePreviousOrder;
    private void OrderPeriodically()
    {
        timeSincePreviousOrder += Time.deltaTime;
        if(timeSincePreviousOrder > timeBetweenOrders)
        {
            agent.destination = order.position;
            timeSincePreviousOrder = 0;
        }
    }

    private void GoToNextPatrolOrder()
    {
        Transform[] patrolOrders = patrolGroup.GetComponent<PatrolGroup>().patrolOrders;

        if ((currentPatrolStage + 1) < patrolOrders.Length)
        {
            currentPatrolStage++;
        }
        else
        {
            currentPatrolStage = 0;
        }

        order = patrolOrders[currentPatrolStage];

        agent.destination = order.position;
    }
}
