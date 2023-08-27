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

    [SerializeField] private float _detectionRadius = 7f;
    public float DetectionRadius => _detectionRadius;

    [SerializeField] private LayerMask _detectionLayer;
    public LayerMask DetectionLayer => _detectionLayer;

    [SerializeField] private Vector3 _targetLookAt;
    public Vector3 TargetLookAt => _targetLookAt;
    public override void Init()
    {
        target = Character.Target;
        

        animator = Character.Animator;
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
        if (!(animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.9f && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack01")))
        {
            Character.SearchClosetTarget();
            target = Character.Target;
        }


        if ((Character.transform.position - target.position).magnitude >= attackRange && (Character.transform.position - target.position).magnitude <= maxChaseRange &&
            !(animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.8f && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack01")))
        {
            Character.Animator.SetBool("IsWalking", true);
            Character.Animator.SetBool("IsAttack", false);
            Character.MoveTo(target.position);
            

        }
        else if ((Character.transform.position - target.position).magnitude >= maxChaseRange ||
            (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack01")))
        {

            IsFinished = true;
            return;
        }
        else
        {
            _targetLookAt = target.position;
            Attack();
        }
    }
    public void Attack()
    {
        Character.Animator.SetBool("IsWalking", false);
        Character.Animator.SetBool("IsAttack", true);

        Character.MoveTo(Character.transform.position);
        animator.transform.LookAt(new Vector3(_targetLookAt.x, animator.transform.position.y, _targetLookAt.z));
    }
    public override void Exit()
    {
        Character.Animator.SetBool("IsWalking", false);
        Character.Animator.SetBool("IsAttack", false);
    }
}
