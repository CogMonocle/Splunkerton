using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fountain : MonoBehaviour {

    public float regenDuration;
    public float regenPerTick;

    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerController p = collision.GetComponent<PlayerController>();
        if(p != null)
        {
            p.ApplyEffect(new FountainRegen(regenDuration, regenPerTick));
        }
    }
}

public class FountainRegen : IEffect
{

    float lifetime;
    float lifeLeft;
    float healPerTick;

    public FountainRegen(float duration, float regen)
    {
        lifetime = duration;
        lifeLeft = lifetime;
        healPerTick = regen;
    }

    public void Regen(ICombatEntity entity)
    {
        entity.Heal(healPerTick);
    }

    public float GetLifeRemaining(float timeSinceLastCheck)
    {
        lifeLeft -= timeSinceLastCheck;
        return lifeLeft;
    }

    public void ResetTimer()
    {
        lifeLeft = lifetime;
    }

    public bool DoesSelfStack()
    {
        return false;
    }

    public OnTickHandler GetTickTrigger()
    {
        return Regen;
    }

    public OnDamageHandler GetDamageTrigger()
    {
        return null;
    }

    public void OnEnd(ICombatEntity entity)
    {
        ;
    }
}
