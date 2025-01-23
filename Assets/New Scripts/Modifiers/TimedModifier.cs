using UnityEngine;

public class TimedModifier : Modifier
{
    [SerializeField] protected float _timer;

    void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
                RemoveEffect();
        }
    }

    public override void ApplyEffect() => _timer = ModifierValue;
}