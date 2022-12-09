using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_Info : MonoBehaviour
{
    public string SpellName;
    public float damage;
    public float speed;
    public float cooldown;
    public float CooldownTimer = -1;
    public float ActiveDuration;
    public int SpellPrice;
    public string[] SpellAttributes;
}

