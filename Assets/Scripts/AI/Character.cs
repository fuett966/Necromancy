using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    [Header("Automatic flags")]

    [SerializeField] private bool _autoChasePlayer;
    public bool _AutoChasePlayer
    {
        get { return _autoChasePlayer; }
        set
        {
            if (_isPatrolingPoint == value)
            {
                _isPatrolingPoint = !value;
            }
            _autoChasePlayer = value;
        }
    }

    [SerializeField] private bool _isAgressive;
    public bool _IsAgressive
    {
        get { return _isAgressive; }
        set { _isAgressive = value; }
    }
    [SerializeField] private bool _isPatrolingPoint;
    public bool _IsPatrolingPoint
    {
        get { return _isPatrolingPoint; }
        set
        {
            if (_autoChasePlayer == value)
            {
                _autoChasePlayer = !value;
            }
            _isPatrolingPoint = value;
        }
    }

    [Header("Initial Parameters")]



    [Header("Initial States")]

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

    [Header("Actual state")]
    [SerializeField] public State CurrentState;

    [Header("Temp Parameters")]

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
        if (characterManager._IsDead)
        {
            SetState(DeathState);
            return;
        }

        if (_isAgressive && SearchEnemyInSphere())
        {
            SearchClosetTarger();

            if (attackState.AttackRange <= (transform.position - target.position).magnitude)
            {
                SetState(ChaseState);
            }
            else
            {
                SetState(AttackState);
            }
        }
        else if (_autoChasePlayer && tag == "Minion")
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

    private void SearchClosetTarger()
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
        target = closestTarget.transform;
    }

    public bool SearchEnemyInSphere()
    {
        if (enemyColliders.Count != 0)
        {
            enemyColliders.Clear();
        }

        List<Collider> allColliders = Physics.OverlapSphere(transform.position, AttackState.MaxChaseRange).ToList();

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
    public void ChangeTarget(Transform newTarget)
    {
        if (characterManager._IsDead)
        {
            return;
        }
        target = newTarget;
        SetState(chaseState);
    }
}