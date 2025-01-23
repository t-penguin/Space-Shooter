using System.Collections;
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
        StartCoroutine(DelayRemove());
    }

    public void Damage(int value)
    {
        _modifierCurrentHealth -= value;
        if (_modifierCurrentHealth < 0)
        {
            RemoveEffect();
        }
    }

    private IEnumerator DelayRemove()
    {
        yield return new WaitForEndOfFrame();
        _player.ToggleBarrier(false);
        base.RemoveEffect();
    }
}