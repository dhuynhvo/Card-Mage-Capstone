// Worked on by Abida Mim

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// uses outside sources as basis

    // makes health applicable on different units
public class UnitHealth
{
    int currentHealth;
    int currentMaxHealth;

        // creates health for unit
    public int Health
    {
        get
        {
            return currentHealth;
        }
        set
        {
            currentHealth = value;
        }
    }

        // sets a max health
    public int MaxHealth
    {
        get
        {
            return currentMaxHealth;
        }
        set
        {
            currentMaxHealth = value;
        }
    }

        // sets health
    public UnitHealth(int health, int maxHealth)
    {
        currentHealth = health;
        currentMaxHealth = maxHealth;
    }

        // takes away health from bar
    public void DmgUnit(int dmgAmount)
    {
        if(currentHealth > 0)
        {
            currentHealth -= dmgAmount;
        }
    }

        // adds health back to bar
        // has not been fully integrated
    public void HealUnit(int healAmount)
    {
        if (currentHealth < currentMaxHealth)
        {
            currentHealth += healAmount;
        }
        if (currentHealth >= currentMaxHealth)
        {
            currentHealth = currentMaxHealth;
        }
    }
}
