using UnityEngine;

public class Barrier : HealthBasedModifier
{
    private void OnEnable()
    {
        GameEvents.ShieldHit += Damage;
    }

    private void OnDisable()
    {
        GameEvents.ShieldHit -= Damage;
    }

    public override void ApplyEffect()
    {
        _player.ToggleBarrier(true);
        base.ApplyEffect();
    }

    public override void RemoveEffect()
    {
        _player.ToggleBarrier(false);
        base.RemoveEffect();
    }

    public void Damage(int value)
    {
        _modifierCurrentHealth -= value;
        if (_modifierCurrentHealth < 0)
        {
            RemoveEffect();
        }
    }
}