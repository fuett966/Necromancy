using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Chase State", menuName = "States/ChaseState")]
public class ChaseState : State
{
    [SerializeField]private Transform target;
    [SerializeField] private float distance = 2f;
    public float Distance => distance;

    [SerializeField] private float maxDistance = 10f;
    public float MaxDistance => maxDistance;

    public override void Init()
    {
        target = Character.Target;
        if (!Character.Animator.GetBool("IsWalking"))
        {
            Character.Animator.SetBool("IsWalking", true);
        }
    }

    public override void Run()
    {
        if (IsFinished)
        {
            Exit();
            return;
        }
        if (Character.CharacterManager._IsDead)
        {
            IsFinished = true;
            return;
        }
        if ((Character.transform.position - target.position).magnitude >= Distance && (Character.transform.position - target.position).magnitude <= MaxDistance)
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
        if(Character.Animator.GetBool("IsWalking"))
        {
            Character.Animator.SetBool("IsWalking", false);
        }
    }
}
