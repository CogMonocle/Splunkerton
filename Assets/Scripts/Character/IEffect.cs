using System;

public delegate void OnTickHandler(ICombatEntity entity);
public delegate void OnDamageHandler(ICombatEntity entity, float amount);

public interface IEffect
{
    float GetLifeRemaining(float timeSinceLastCheck);

    void ResetTimer();

    bool DoesSelfStack();

    OnTickHandler GetTickTrigger();

    OnDamageHandler GetDamageTrigger();

    void OnEnd(ICombatEntity entity);
}
