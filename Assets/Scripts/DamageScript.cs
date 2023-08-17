using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    [HideInInspector]public float damageAmount = 10f;
    private Collider damageCollider;
    public string[] enemyTags;

    private void Start() 
    {
        damageCollider = transform.GetComponent<Collider>();
        damageCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider hitTarget)
    {
        for (int i = 0; i < enemyTags.Length; i++)
        {
            if (hitTarget.transform.TryGetComponent<IDamagable>(out IDamagable target) && hitTarget.tag == enemyTags[i])
            {
                target.TakeDamage(damageAmount);
                break;
            }
        }
    }

    public void EnableDamageCollider()
    {
        damageCollider.enabled = true;
    }
    public void DisableDamageCollider()
    {
        damageCollider.enabled = false;
    }
}
