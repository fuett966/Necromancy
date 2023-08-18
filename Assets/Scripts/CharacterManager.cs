using UnityEngine;

public class CharacterManager : MonoBehaviour, IDamagable
{
    [Header("Character Parameters")]

    [SerializeField] private float initialHealth = 100f;
    public float InitialHealth => initialHealth;

    [SerializeField] private float damage = 10f;
    public float Damage => damage;

    [SerializeField] private string tagCorpse;
    public string TagCorpse => tagCorpse;

    [SerializeField] private string[] enemyTags;
    public string[] EnemyTags => enemyTags;

    [SerializeField] private CharacterInfo characterInfo;
    public CharacterInfo CharacterInfo => characterInfo;

    [Header("Temp Parameters")]
    [SerializeField] private bool _isDead;
    public bool _IsDead => _isDead;

    [SerializeField] private float tempHealth;
    public float TempHealth => tempHealth;

    [SerializeField] private DamageScript damageScript;
    public DamageScript DamageScript => damageScript;

    [SerializeField] private Animator animator;
    public Animator Animator => animator;
    [SerializeField] private Character character;
    public Character Character => character;


    private void Awake()
    {
        character = GetComponent<Character>();
        damageScript = GetComponentInChildren<DamageScript>();
        animator = GetComponent<Animator>();
        characterInfo = GetComponent<CharacterInfo>();

        damageScript.enemyTags = enemyTags;
        
        
    }
    void Start()
    {
        CheckCharacterLiveState();

        ChangeDamage();
    }

    private void CheckCharacterLiveState()
    {
        if (characterInfo.CurrentState == null || characterInfo.CurrentState == "")
        {
            characterInfo.CurrentState = "Corpse";
            _isDead = true;
            tempHealth = 0f;
            return;
        }
        if (characterInfo.CurrentState == "Corpse")
        {
            _isDead = true;
            tempHealth = 0f;
            return;
        }

        tempHealth = initialHealth;
    }

    void Update()
    {
        CheckHealth();

    }
    public void ChangeDamage()
    {
        damageScript.damageAmount = damage;
    }

    private void CheckHealth()
    {
        if (tempHealth <= 0)
        {
            _isDead = true;
            Death();
        }
        else
        {
            _isDead = false;
        }
    }
    private void Death()
    {
        gameObject.tag = tagCorpse;
    }

    public void TakeDamage(float damageAmount)
    {
        tempHealth -= damageAmount;

        if ((animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("Damaged")) || 
            animator.GetCurrentAnimatorStateInfo(0).IsName("Attack01") || 
            animator.GetCurrentAnimatorStateInfo(0).IsName("Death01") || 
            !animator.GetBool("IsDead"))
        {
            animator.SetBool("IsDamaged", false);
        }
        else
        {
            animator.SetBool("IsDamaged", true);
        }


    }
    public void ResetHealth()
    {
        tempHealth = initialHealth;
    }
    public void CorpseNecromancy()
    {
        tag = "Minion";

        character._AutoChasePlayer = true;
        character._IsAgressive = true;

        enemyTags = new string[1] { "Enemy" };
        damageScript.enemyTags = enemyTags;

        ResetHealth();
    }
    public void OpenDamageCollider()
    {
        damageScript.EnableDamageCollider();
    }
    public void CloseDamageCollider()
    {
        damageScript.DisableDamageCollider();
    }
}
