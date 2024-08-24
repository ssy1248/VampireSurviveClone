using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponBehavior : MonoBehaviour
{
    public WeaponScriptableObject weaponData;

    public float destroyAfterSeconds;

    //current Stats
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected float currentPirece;
    void Awake()
    {
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCooldownDuration = weaponData.CooldownDuration;
        currentPirece = weaponData.Pierce;
    }

    public float GetCurrentDamage()
    {
        return currentDamage *= FindObjectOfType<PlayerStats>().currentMight;
    }

    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            EnemyStats enemy = collision.GetComponent<EnemyStats>();
            enemy.TakeDamage(GetCurrentDamage());
        }
        else if (collision.CompareTag("Prop"))
        {
            if (collision.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(GetCurrentDamage());
            }
        }
    }
}
