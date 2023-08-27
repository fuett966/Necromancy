using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Data", menuName = "States/RandomMoveState")]
public class RandomMoveState : State
{
    public float MaxDistance = 5f;
    public float MinDistance = 1f;
    public float completeDistance = 0.55f;
    public float completeTimer = 5f;
    public bool aroundTarget = true;
    [SerializeField] private float tempCompleteTimer;
    private Transform randomMoveTarget;

    Vector3 randomPosition;
    NavMeshHit navMeshHit;


    public override void Init()
    {
        randomMoveTarget = Character.RandomMoveTarget;
        tempCompleteTimer = completeTimer;

        CreateRandomPoint();

        if (!Character.Animator.GetBool("IsWalking"))
        {
            Character.Animator.SetBool("IsWalking", true);
        }
    }

    public override void Run()
    {
        if (IsFinished)
        {
            return;
        }
        if (!Character.CharacterManager._IsAlive)
        {
            IsFinished = true;
            return;
        }
        if (Character.SearchEnemyInSphere())
        {
            IsFinished = true;
            return;
        }


        if ((randomPosition - Character.transform.position).magnitude > completeDistance)
        {
            Character.MoveTo(randomPosition);
        }
        else
        {
            IsFinished = true;
            Character.SetWaitState();
        }

        if (tempCompleteTimer <= 0)
        {
            IsFinished = true;
        }
        tempCompleteTimer -= Time.deltaTime;
    }

    public void CreateRandomPoint()
    {
        if (aroundTarget)
        {
            do
            {
                NavMesh.SamplePosition(Random.insideUnitSphere * MaxDistance + randomMoveTarget.position, out navMeshHit, MaxDistance, NavMesh.AllAreas);

                randomPosition = navMeshHit.position;
                // + new Vector3(x: 0f, y: 0.5f, z: 0f)
            }
            while ((randomPosition - randomMoveTarget.position).magnitude > MaxDistance);
        }
        else
        {
            do
            {
                NavMesh.SamplePosition(Random.insideUnitSphere * MaxDistance + Character.transform.position, out navMeshHit, MaxDistance, NavMesh.AllAreas);

                randomPosition = navMeshHit.position;
                // + new Vector3(x: 0f, y: 0.5f, z: 0f)
            }
            while ((randomPosition - Character.transform.position).magnitude > MaxDistance);
        }

    }
    public override void Exit()
    {
        if (Character.Animator.GetBool("IsWalking"))
        {
            Character.Animator.SetBool("IsWalking", false);
        }
    }
}
