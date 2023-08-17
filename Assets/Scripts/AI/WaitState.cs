using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Data", menuName = "States/WaitState")]

public class WaitState : State
{
    public float waitingTime = 2f;
    private float tempWaitingTime;
    public override void Init()
    {
        tempWaitingTime = waitingTime;
        if(Character.Animator.GetBool("IsWalking"))
        {
            Character.Animator.SetBool("IsWalking", false);
        }
    }

    public override void Run()
    {
        if (IsFinished)
        {
            return;
        }
        if (Character.CharacterManager._IsDead)
        {
            IsFinished = true;
            return;
        }

        if (tempWaitingTime >= 0)
        {
            tempWaitingTime -= Time.deltaTime;
        }
        else
        {
            IsFinished = true;
        }
    }
    public override void Exit() 
    {
        
    }
}
