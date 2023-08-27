using UnityEngine;

public class CharacterManager : EntityManager, IDamagable
{
    #region Variables

    [SerializeField] private float initialHealth = 100f;
    public float InitialHealth => initialHealth;

    [SerializeField] private float damage = 10f;
    public float Damage => damage;

    [SerializeField] private string[] enemyTags;
    public string[] EnemyTags => enemyTags;



    [SerializeField] private bool _isAlive;
    public bool _IsAlive => _isAlive;

    [SerializeField] private float tempHealth;
    public float TempHealth => tempHealth;

    #region Components

    [SerializeField] private CharacterInfo characterInfo;

    [SerializeField] private DamageScript damageScript;

    [SerializeField] private Animator animator;

    [SerializeField] private Character character;

    #endregion Components

    [SerializeField] private AudioClip _wasDamaged;
    [SerializeField] private AudioClip _walking;

    #endregion Variables

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
            _isAlive = false;
            tempHealth = 0f;
            return;
        }
        if (characterInfo.CurrentState == "Corpse")
        {
            _isAlive = false;
            tempHealth = 0f;
            return;
        }

        tempHealth = initialHealth;
    }
    public void ChangeDamage()
    {
        damageScript.damageAmount = damage;
    }

    void Update()
    {
        if (animator.GetBool("IsWalking") && !GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().PlayOneShot(_walking);
        }
        else
        {

        }
        CheckHealth();
    }


    private void CheckHealth()
    {
        if (tempHealth <= 0)
        {
            _isAlive = false;
            gameObject.tag = "Corpse";
        }
        else
        {
            _isAlive = true;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        tempHealth -= damageAmount;
        character.HandleDetection();

        if ((animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("Damaged")) ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Attack01") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Death01") ||
            !animator.GetBool("IsDead"))
        {
            animator.SetBool("IsDamaged", false);
            Debug.Log("Ëîõ");
        }
        else
        {
animator.SetBool("IsDamaged", true);
        }
        GetComponent<AudioSource>().PlayOneShot(_wasDamaged);
    }
    public void CorpseNecromancy()
    {
        tag = "Minion";


        enemyTags = new string[1] { "Enemy" };
        damageScript.enemyTags = enemyTags;

        tempHealth = initialHealth;
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
