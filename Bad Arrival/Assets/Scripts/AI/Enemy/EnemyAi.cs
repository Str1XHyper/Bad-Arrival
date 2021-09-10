using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public EnemyObject enemyObject;

    [SerializeField] private Transform order;
    [SerializeField] private Transform combatOrder;
    [SerializeField] private GameObject patrolGroup;
    private int currentPatrolStage = 0;
    private GameObject player;
    [SerializeField] public GameObject audioManager;

    [SerializeField] private AiBehaviourState BehaviourState;
    [SerializeField] private float aggroMoveSpeed;
    [SerializeField] private float patrolMoveSpeed;

    [SerializeField] private float startAggroRange;
    [SerializeField] private float loseAggroRange;
    [SerializeField] private float distanceBetweenAiAndPlayer;

    [SerializeField] private float attackRange;
    private float timeSincePreviousAttack = 99999; //High amount on purpose
    [SerializeField] private float timeBetweenAttacks;

    private AbilityHandler abilityHandler;

    private NavMeshAgent agent;


    private float timeSincePreviousOrder;
    private float timeBetweenOrders = 0.05f;
    private float timeSinceLastDistanceCheck;
    private float timeBetweenDistanceChecks = 0.05f;

    public enum AiBehaviourState
    {
        Idle, 
        Patrol,
        Aggro,
        Attack,
        Return,
        Die,
        Dying
    }

    // Start is called before the first frame update
    void Start()
    {
        aggroMoveSpeed = enemyObject.aggroMoveSpeed;
        patrolMoveSpeed = enemyObject.patrolMoveSpeed;
        attackRange = enemyObject.attackRange;
        timeBetweenAttacks = enemyObject.timeBetweenAttacks;
        startAggroRange = enemyObject.startAggroRange;
        loseAggroRange = enemyObject.loseAggroRange;

        player = Player.instance.gameObject;

        BehaviourState = AiBehaviourState.Idle;
        agent = GetComponent<NavMeshAgent>();
        //agent.destination = order.position;
        agent.speed = patrolMoveSpeed;
        abilityHandler = GetComponent<AbilityHandler>();
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
            case AiBehaviourState.Die:
                Die();
                break;
            case AiBehaviourState.Dying:
                Dying();
                break;
            default:
                break;
        }

        UpdateDistanceBetweenAiAndPlayer();
    }

    private void Idle()
    {
        CheckIfPlayerIsInsideStartAggroRange();

        if (patrolGroup != null)
        {
            BehaviourState = AiBehaviourState.Patrol;
        }
    }

    private void Patrol()
    {
        CheckIfPlayerIsInsideStartAggroRange();

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
        CheckIfPlayerIsOutsideLoseAggroRange();
        OrderPeriodically();
        if (CheckIfAttackRangeReached())
        {
            agent.destination = transform.position;
            BehaviourState = AiBehaviourState.Attack;
        }
    }

    private void Attack()
    {
        //Keep rotating towards the Target (Player)
        Vector3 direction = (combatOrder.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 9f);

        AttackPeriodically();
        CheckIfPlayerIsOutsideLoseAggroRange();
    }

    private void Return()
    {
        CheckIfPlayerIsInsideStartAggroRange();

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {

            if (patrolGroup != null)
            {
                
                BehaviourState = AiBehaviourState.Patrol;
            }
            else
            {
                BehaviourState = AiBehaviourState.Idle;
            }
        }
    }

    private void Dying()
    {
        //Death Animation
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void CheckIfPlayerIsInsideStartAggroRange()
    {
        if(distanceBetweenAiAndPlayer < startAggroRange)
        {
            StartAggro();
        }
    }

    private void CheckIfPlayerIsOutsideLoseAggroRange()
    {
        if (distanceBetweenAiAndPlayer > loseAggroRange)
        {
            StopAggro();
        }
    }

    public void StartAggro()
    {
        combatOrder = player.transform;
        BehaviourState = AiBehaviourState.Aggro;
        agent.speed = aggroMoveSpeed;
    }

    public void StopAggro()
    {
        combatOrder = null;
        agent.destination = order.position;
        BehaviourState = AiBehaviourState.Return;
        agent.speed = patrolMoveSpeed;
    }

    
    private void OrderPeriodically()
    {
        timeSincePreviousOrder += Time.deltaTime;
        if(timeSincePreviousOrder > timeBetweenOrders)
        {
            if(combatOrder == null)
            {
                agent.destination = order.position;
            }
            else
            {
                agent.destination = combatOrder.position;
            }
            
            timeSincePreviousOrder = 0;
        }
    }


    private void UpdateDistanceBetweenAiAndPlayer()
    {
        timeSinceLastDistanceCheck += Time.deltaTime;
        if (timeSinceLastDistanceCheck > timeBetweenDistanceChecks)
        {
            distanceBetweenAiAndPlayer = Vector3.Distance(transform.position, player.transform.position);
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

    private bool CheckIfAttackRangeReached()
    {
        if (!agent.pathPending && agent.remainingDistance < attackRange)
        {
            return true;
        }

        return false;
    }

    private void AttackPeriodically()
    {
        timeSincePreviousAttack += Time.deltaTime;
        if (timeSincePreviousAttack > timeBetweenAttacks)
        {
            timeSincePreviousAttack = 0;
            abilityHandler.Attack();
            audioManager.GetComponent<EnemyAudioManager>().PlayShootAudio();
        }
    }
}
