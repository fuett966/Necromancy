using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerManager : EntityManager , IDamagable
{
    #region Variables

    [SerializeField] private float _initialHealth = 100f;
    public float InitialHealth => _initialHealth;

    [SerializeField] private float _temporaryHealth;
    public float TemporaryHealth => _temporaryHealth;

    [SerializeField] private bool _isAlive;
    public bool IsAlive => _isAlive;

    #endregion Variables

    public void TakeDamage(float damageAmount)
    {
        _temporaryHealth -= damageAmount;
    }

    void Start()
    {
        _temporaryHealth = _initialHealth;
    }
    

    void Update()
    {
        if(_isAlive && _temporaryHealth <= 0)
        {
            _isAlive = false;
            // Death
        }
    }
}
