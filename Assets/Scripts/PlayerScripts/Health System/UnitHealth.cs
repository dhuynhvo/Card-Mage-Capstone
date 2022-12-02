using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealth
{
    int currentHealth;
    int currentMaxHealth;

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

    public UnitHealth(int health, int maxHealth)
    {
        currentHealth = health;
        currentMaxHealth = maxHealth;
    }

    public void DmgUnit(int dmgAmount)
    {
        if(currentHealth > 0)
        {
            currentHealth -= dmgAmount;
        }
    }

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
