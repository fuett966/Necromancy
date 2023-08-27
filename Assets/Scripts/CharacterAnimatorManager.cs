using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public Animator Animator => animator;

    public bool IsWalking;
    public bool IsAttack;
    public bool IsDead;
    public bool IsDamaged;


    private void Awake()
    {
        animator = GetComponent<Animator>();

        IsWalking = animator.GetBool("IsWalking");
        IsAttack = animator.GetBool("IsAttack");
        IsDead = animator.GetBool("IsDead");
        IsDamaged = animator.GetBool("IsDamaged");
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

}
