using UnityEngine;

public abstract class HealthBasedModifier : Modifier
{
    [SerializeField] protected int _modifierCurrentHealth;

    public override void ApplyEffect() => _modifierCurrentHealth = (int)ModifierValue;
}