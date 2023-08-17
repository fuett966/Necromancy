using UnityEngine;
[CreateAssetMenu(fileName = "New Attack State", menuName = "States/AttackState")]
public class AttackState : State
{
    private Transform target;
    private Animator animator;
    [SerializeField] private float attackRange = 2f;
    public float AttackRange => attackRange;
    [SerializeField] private float maxChaseRange = 5f;
    public float MaxChaseRange => maxChaseRange;
    public override void Init()
    {
        target = Character.Target;
        animator = Character.Animator;
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

        if ((Character.transform.position - target.position).magnitude >= attackRange ||
            (Character.transform.position - target.position).magnitude >= maxChaseRange ||
            (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack01")))
        {
            IsFinished = true;
            return;
        }
        else
        {
            Attack();
        }
    }
    public void Attack()
    {
        if (Character.Animator.GetBool("IsWalking"))
        {
            Character.Animator.SetBool("IsWalking", false);
        }
        if (!Character.Animator.GetBool("IsAttack"))
        {
            Character.Animator.SetBool("IsAttack", true);
        }
        Character.MoveTo(Character.transform.position);
        animator.transform.LookAt(new Vector3(target.position.x, animator.transform.position.y, target.position.z));
    }
    public override void Exit()
    {
        if (Character.Animator.GetBool("IsWalking"))
        {
            Character.Animator.SetBool("IsWalking", false);
        }
        if (Character.Animator.GetBool("IsAttack"))
        {
            Character.Animator.SetBool("IsAttack", false);
        }
    }
}
