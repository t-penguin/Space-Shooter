using System;
using UnityEditor;
using UnityEngine;

public static class GameEvents
{
    public static event Action PlayerDestroyed;
    public static void DestroyPlayer() => PlayerDestroyed?.Invoke();

    public static event Action AsteroidDestroyed;
    public static void DestroyAsteroid() => AsteroidDestroyed?.Invoke();

    public static event Action<ModifierType, float> ModifierPickedUp;
    public static void PickUpModifier(ModifierType modifier, float value) 
        => ModifierPickedUp?.Invoke(modifier, value);

    public static event Action<int> ShieldHit;
    public static void HitShield(int damage) => ShieldHit?.Invoke(damage);
}