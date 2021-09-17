using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public EnemyObject enemyObject;
    public LayerMask raycastLayers;

    [SerializeField] private Transform order;
    [SerializeField] private Transform combatOrder;
    [SerializeField] private Transform playerLastKnownPosition;
    [SerializeField] private GameObject patrolGroup;
    [SerializeField] private Vector3 idlePosition;
    private int currentPatrolStage = 0;
    private GameObject player;
    [SerializeField] public GameObject audioManager;

    [SerializeField] private AiBehaviourState behaviourState;
    [SerializeField] private float aggroMoveSpeed;
    [SerializeField] private float patrolMoveSpeed;

    [SerializeField] private float startAggroRange;
    [SerializeField] private float loseAggroRange;
    [SerializeField] private float distanceBetweenAiAndPlayer;

    [SerializeField] private float attackRange;
    private float timeSincePreviousAttack = 99999; //High amount on purpose
    [SerializeField] private float timeBetweenAttacks;

    [SerializeField] private float searchResetTime;
    private float searchTimeElapsed;

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
        Dying,
        Searching
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
        searchResetTime = enemyObject.searchResetTime;

        player = Player.instance.gameObject;

        behaviourState = AiBehaviourState.Idle;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = patrolMoveSpeed;
        abilityHandler = GetComponent<AbilityHandler>();

        if (patrolGroup == null)
        {
            idlePosition = transform.position;
        }
        else
        {
            order = patrolGroup.GetComponent<PatrolGroup>().patrolOrders[0];
            agent.destination = order.position;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (behaviourState)
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
            case AiBehaviourState.Searching:
                Searching();
                break;
            default:
                break;
        }

        UpdateDistanceBetweenAiAndPlayer();
    }

    private void Idle()
    {
        IfPlayerIsInsideStartAggroRange();

        if (patrolGroup != null)
        {
            behaviourState = AiBehaviourState.Patrol;
        }

        agent.destination = idlePosition; //Shouldn't run every update
    }

    private void Patrol()
    {
        IfPlayerIsInsideStartAggroRange();

        if (!agent.pathPending && agent.remainingDistance < 1.5f)
        {
            if(patrolGroup != null)
            {
                GoToNextPatrolOrder();
            }
        }
            
    }

    private void Aggro()
    {

        IfPlayerIsOutsideLoseAggroRange();
        OrderPeriodically();
        if (CheckIfAttackRangeReached())
        {
            agent.destination = transform.position;
            behaviourState = AiBehaviourState.Attack;
        }
    }

    private void Attack()
    {

        if (combatOrder != null)
        {
            if (IfVisionIsObstructed(transform.position, combatOrder.transform.position))
            {
                StartSearching();
            }

            //Keep rotating towards the Target (Player)
            Vector3 direction = (combatOrder.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 9f);

            AttackPeriodically();
            
        }

        IfPlayerIsOutsideLoseAggroRange();
    }

    private void Return()
    {
        IfPlayerIsInsideStartAggroRange();

        if(agent.destination == null)
        {
            if(patrolGroup == null)
            {
                behaviourState = AiBehaviourState.Idle;
            }
            else
            {
                behaviourState = AiBehaviourState.Patrol;
            }
        }

        if (!agent.pathPending && agent.remainingDistance < 1.5f)
        {

            if (patrolGroup != null)
            {
                
                behaviourState = AiBehaviourState.Patrol;
            }
            else
            {
                behaviourState = AiBehaviourState.Idle;
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

    private void Searching()
    {
        searchTimeElapsed += Time.deltaTime;

        if(searchTimeElapsed > searchResetTime)
        {
            playerLastKnownPosition = null;
            behaviourState = AiBehaviourState.Return;
        }
    }

    private void StartSearching()
    {
        searchTimeElapsed = 0;
        playerLastKnownPosition = player.transform;
        agent.destination = playerLastKnownPosition.position;
        behaviourState = AiBehaviourState.Searching;
    }

    private void IfPlayerIsInsideStartAggroRange()
    {
        if(distanceBetweenAiAndPlayer < startAggroRange)
        {
            if (!IfVisionIsObstructed(transform.position, player.transform.position))
            {
                StartAggro();
            }
        }
    }

    private void IfPlayerIsOutsideLoseAggroRange()
    {
        if (distanceBetweenAiAndPlayer > loseAggroRange)
        {
            StopAggro();
        }
    }

    private bool IfVisionIsObstructed(Vector3 startPoint, Vector3 endPoint)
    {
        Vector3 direction = (endPoint - startPoint).normalized;
        RaycastHit hit;

        if (Physics.Raycast(startPoint, direction, out hit, loseAggroRange, raycastLayers))
        {
            Debug.DrawRay(startPoint, direction * hit.distance, Color.red);
            if (hit.transform.CompareTag("Player"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
    }

    public void StartAggro()
    {
        combatOrder = player.transform;
        behaviourState = AiBehaviourState.Aggro;
        agent.speed = aggroMoveSpeed;
    }

    public void StopAggro()
    {
        combatOrder = null;
        if(agent.destination != null && order != null)
        {
            agent.destination = order.position;
            behaviourState = AiBehaviourState.Return;
        }
        else
        {
            behaviourState = AiBehaviourState.Idle;
        }
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
