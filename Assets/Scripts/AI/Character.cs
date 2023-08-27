using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    #region States

    [SerializeField] private State startState;
    public State StartState => startState;

    [SerializeField] private RandomMoveState randomMoveState;
    public RandomMoveState RandomMoveState => randomMoveState;
    [SerializeField] private WaitState waitState;
    public WaitState WaitState => waitState;
    [SerializeField] private ChaseState chaseState;
    public ChaseState ChaseState => chaseState;
    [SerializeField] private AttackState attackState;
    public AttackState AttackState => attackState;
    [SerializeField] private DeathState deathState;
    public DeathState DeathState => deathState;

    [SerializeField] public State CurrentState;
    #endregion States

    [SerializeField] private float minimumDetectionAngle = -45f;
    public float MinimumDetectionAngle => minimumDetectionAngle;

    [SerializeField] private float maximumDetectionAngle = 45f;
    public float MaximumDetectionAngle => maximumDetectionAngle;

    [SerializeField] private GameObject playerGM;
    public GameObject PlayerGM => playerGM;

    [SerializeField] private Animator animator;
    public Animator Animator => animator;

    [SerializeField] private Transform target;
    public Transform Target => target;

    [SerializeField] private Transform randomMoveTarget;
    public Transform RandomMoveTarget => randomMoveTarget;

    [SerializeField] private CharacterManager characterManager;
    public CharacterManager CharacterManager => characterManager;

    [SerializeField] private List<Collider> enemyColliders;
    public List<Collider> EnemyColliders => enemyColliders;



    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public NavMeshPath navMeshPath;
    private void Awake()
    {
        playerGM = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        navMeshPath = new NavMeshPath();
        characterManager = GetComponent<CharacterManager>();

        // target = transform;
    }

    void Start()
    {
        SetState(StartState);
    }

    void Update()
    {

        if (!CurrentState.IsFinished)
        {
            CurrentState.Run();
        }
        else
        {
            CurrentState.Exit();
            ChoiceState();
        }
    }

    public void SetState(State state)
    {
        CurrentState = Instantiate(state);
        CurrentState.Character = this;
        CurrentState.Init();
    }

    public void MoveTo(Vector3 position)
    {
        agent.SetDestination(position);
    }

    public void SetWaitState()
    {
        SetState(WaitState);
    }
    public void ChoiceState()
    {
        if (!characterManager._IsAlive)
        {
            SetState(DeathState);
            return;
        }

        if ((SearchEnemyInSphere() && attackState.TargetLookAt != null) || SearchEnemyInCone())
        {
            Debug.Log("isATTACK");


            SearchClosetTarget();

            SetState(AttackState);

        }
        else if (tag == "Minion")
        {
            if (Vector3.Distance(transform.position, playerGM.transform.position) > ChaseState.MaxDistance)
            {
                target = PlayerGM.transform;
                SetState(ChaseState);
            }
            else
            {
                randomMoveTarget = PlayerGM.transform;
                SetState(RandomMoveState);
            }
        }
        else
        {
            randomMoveTarget = transform;
            SetState(RandomMoveState);
        }
    }



    public bool SearchEnemyInCone()
    {
        if (enemyColliders.Count != 0)
        {
            enemyColliders.Clear();
        }

        List<Collider> allColliders = Physics.OverlapSphere(transform.position, AttackState.DetectionRadius, AttackState.DetectionLayer).ToList();


        foreach (var collider in allColliders)
        {
            for (int i = 0; i < characterManager.EnemyTags.Length; i++)
            {
                if (collider.gameObject.tag == characterManager.EnemyTags[i] && collider != gameObject.GetComponent<Collider>())
                {
                    EntityManager entityManager = collider.transform.GetComponent<EntityManager>();
                    if (entityManager != null)
                    {
                        // CHECK FOR TEAM ID
                        Vector3 targetDirection = entityManager.transform.position - transform.position;
                        float viewableAngle = Vector3.Angle(targetDirection, transform.forward);
                        if (viewableAngle > minimumDetectionAngle && viewableAngle < maximumDetectionAngle)
                        {
                            enemyColliders.Add(collider);
                            //target = entityManager.transform;
                        }
                    }
                }
            }
        }

        allColliders.Clear();

        if (enemyColliders.Count == 0)
        {
            return false;
        }
        else
        {
            return true;
        }

    }

    public bool SearchEnemyInSphere()
    {
        if (enemyColliders.Count != 0)
        {
            enemyColliders.Clear();
        }

        List<Collider> allColliders = Physics.OverlapSphere(transform.position, AttackState.DetectionRadius, AttackState.DetectionLayer).ToList();


        foreach (var collider in allColliders)
        {
            for (int i = 0; i < characterManager.EnemyTags.Length; i++)
            {
                if (collider.gameObject.tag == characterManager.EnemyTags[i] && collider != gameObject.GetComponent<Collider>())
                {
                    enemyColliders.Add(collider);
                }
            }
        }

        allColliders.Clear();

        if (enemyColliders.Count == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public void SearchClosetTarget()
    {
        Collider closestTarget = null;
        foreach (var collider in enemyColliders)
        {
            if (closestTarget == null)
            {
                closestTarget = collider;
            }
            else
            {
                if ((closestTarget.transform.position - transform.position).magnitude > (collider.transform.position - transform.position).magnitude)
                {
                    closestTarget = collider;
                }
            }
        }
        if (closestTarget != null)
        {
            target = closestTarget.transform;
        }

    }
    public void SetAttackState()
    {

        CurrentState.Exit();
        SetState(AttackState);
    }

    public void ChangePosition(Transform newPosition)
    {
        if (CurrentState != AttackState && characterManager._IsAlive)
        {
            target = newPosition;
            CurrentState.Exit();
            SetState(chaseState);
            return;
        }
    }
}