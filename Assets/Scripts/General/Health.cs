using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int currentHp;
    [Tooltip("My health points"), SerializeField]
    private int maxHp = 1;

    [SerializeField]
    private UnityEngine.Events.UnityEvent onDeath;

    public int MaxHP { get => maxHp; }

    void Start()
    {
        currentHp = maxHp; // Establecer vida al max. al iniciar
    }

    public void Heal(int hp)
    {
        this.currentHp += hp;
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
    }
    public void TakeDamage(int hp)
    {
        this.currentHp -= hp;
        if (currentHp <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        this.currentHp = 0;
        onDeath?.Invoke();
    }
    public void DestroyOnDeath()
    {
        Destroy(this.gameObject, .2f);
    }
}
