using UnityEngine;
using System.Collections;

public interface ICombatEntity
{
    void Heal(float amount);

    void Damage(float amount);

    void Knockback(Vector3 source, float strength);
}
