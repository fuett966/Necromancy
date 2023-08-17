using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DeathState", menuName = "States/DeathState")]
public class DeathState : State
{
    public override void Init()
    {
        Character.Animator.SetBool("IsDead", true);
        Character.MoveTo(Character.transform.position);
        Character.agent.enabled = false;
    }

    public override void Run()
    {
        if (IsFinished)
        {
            Exit();
            return;
        }
        if (!Character.GetComponent<CharacterManager>()._IsDead)
        {
            IsFinished = true;
        }
    }
    public override void Exit()
    {
        Character.Animator.SetBool("IsDead", false);
        Character.agent.enabled = true;
    }
}
