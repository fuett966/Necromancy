using UnityEngine;
[CreateAssetMenu(fileName = "New Chase State", menuName = "States/ChaseState")]
public class ChaseState : State
{
    [SerializeField] private Transform target;
    [SerializeField] private float distance = 2f;
    public float Distance => distance;

    [SerializeField] private float maxDistance = 10f;
    public float MaxDistance => maxDistance;

    public override void Init()
    {
        target = Character.Target;
        Character.Animator.SetBool("IsWalking", true);

    }

    public override void Run()
    {
        if (IsFinished)
        {
            return;
        }

        if (!Character.CharacterManager._IsAlive || Character.SearchEnemyInSphere())
        {
            IsFinished = true;
            return;
        }


        if ((Character.transform.position - target.position).magnitude >= MaxDistance)
        {
            Character.MoveTo(target.position);
        }
        else
        {
            IsFinished = true;
        }

    }
    public override void Exit()
    {
        Character.Animator.SetBool("IsWalking", false);
    }
}
