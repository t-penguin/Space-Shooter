public class SpeedyShot : TimedModifier
{
    public override void ApplyEffect()
    {
        _player.ScaleFireRate(2);
        base.ApplyEffect();
    }

    public override void RemoveEffect()
    {
        _player.ScaleFireRate(0.5f);
        base.RemoveEffect();
    }
}
