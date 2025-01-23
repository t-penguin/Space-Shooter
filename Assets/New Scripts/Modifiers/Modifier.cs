using UnityEngine;

public abstract class Modifier : MonoBehaviour
{
    [field:SerializeField] public float ModifierValue { get; set; }

    protected PlayerController1 _player;

    private void Awake()
    {
        _player = GetComponent<PlayerController1>();
        if (_player == null)
        {
            Debug.LogWarning("Modifier is not attached to a player!");
            return;
        }
    }

    public abstract void ApplyEffect();
    public virtual void RemoveEffect() => Destroy(this);
}

public enum ModifierType
{
    SpeedyShot,
    PiercingShot,
    Barrier
}