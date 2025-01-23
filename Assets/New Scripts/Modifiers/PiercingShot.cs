public class PiercingShot : TimedModifier
{
    public override void ApplyEffect()
    {
        _player.PierceShots(true);
        base.ApplyEffect();
    }

    public override void RemoveEffect()
    {
        _player.PierceShots(false);
        base.RemoveEffect();
    }
}