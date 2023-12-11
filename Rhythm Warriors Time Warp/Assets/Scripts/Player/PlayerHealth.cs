using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        // check for death
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // game over
        Debug.Log("Player died");
        // trigger a game over screen, need to be implemented
    }
}
