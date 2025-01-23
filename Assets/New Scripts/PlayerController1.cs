using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController1 : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _speed = 1f;
    [SerializeField] private Boundary1 _boundary;

    [Header("Firing")]
    [SerializeField] private GameObject _shot;
    [SerializeField] private Transform _shotSpawn;
    [SerializeField] private float _fireRate;

    [Header("Shield")]
    [SerializeField] private GameObject _barrier;
    [field:SerializeField] public bool IsShielded { get; private set; }

    private Vector3 _movement;
    private Rigidbody _rigidbody;
    private AudioSource _audioSource;
    private float _nextFireTime;
    private bool _tryFire;

    #region Monobehaviour Callbacks

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        _shot.tag = "Bolt";
        _movement = new Vector3();
        _nextFireTime = 0;
        _barrier.SetActive(false);
        IsShielded = false;
    }

    private void Update()
    {
        if (_tryFire)
            FireShot();
    }

    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = _movement;
        ClampPosition();
    }

    private void OnEnable()
    {
        GameEvents.ModifierPickedUp += OnModifierPickedUp;
    }

    private void OnDisable()
    {
        GameEvents.ModifierPickedUp -= OnModifierPickedUp;
    }

    #endregion

    #region Player Input Callbacks

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();
        _movement.x = movement.x * _speed;
        _movement.z = movement.y * _speed;
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _tryFire = true;
            return;
        }

        if (context.canceled)
        {
            _tryFire = false;
            return;
        }
    }

    #endregion

    #region Game Event Callbacks

    private void OnModifierPickedUp(ModifierType modifier, float value)
    {
        Debug.Log($"Adding modifier {modifier.ToString()} with value {value}");
        Modifier mod = null;
        switch (modifier)
        {
            case ModifierType.SpeedyShot:
                mod = AddModifier<SpeedyShot>();
                break;
            case ModifierType.PiercingShot:
                mod = AddModifier<PiercingShot>();
                break;
            case ModifierType.Barrier:
                mod = AddModifier<Barrier>();
                break;
        }
        mod.ModifierValue = value;
        mod.ApplyEffect();
    }

    #endregion

    #region Modifier Methods

    public void ScaleFireRate(float scale) => _fireRate /= scale;

    public void PierceShots(bool pierce) => _shot.tag = pierce ? "Piercing" : "Bolt";

    public void ToggleBarrier(bool hasBarrier)
    {
        _barrier.SetActive(hasBarrier);
        IsShielded = hasBarrier;
    }

    #endregion

    #region Helper Methods

    private void FireShot()
    {
        if (Time.time < _nextFireTime)
            return;

        _nextFireTime = Time.time + _fireRate;
        Instantiate(_shot, _shotSpawn.position, _shotSpawn.rotation);
        _audioSource.Play();
    }

    private void ClampPosition()
    {
        Vector3 position = _rigidbody.position;
        bool outOfBoundsX = position.x < _boundary.Left || position.x > _boundary.Right;
        bool outOfBoundsZ = position.z < _boundary.Down || position.z > _boundary.Up;
        if (outOfBoundsX || outOfBoundsZ)
            Debug.LogWarning("Out of bounds! Please return to the play area!");

        position.x = Mathf.Clamp(position.x, _boundary.Left, _boundary.Right);
        position.z = Mathf.Clamp(position.z, _boundary.Down, _boundary.Up);
        _rigidbody.position = position;
    }

    private T AddModifier<T>()
    {
        T component = GetComponent<T>();
        if (component == null)
            gameObject.AddComponent(typeof(T));

        return GetComponent<T>();
    }

    #endregion
}

[System.Serializable]
public class Boundary1
{
    public float Up;
    public float Down;
    public float Left;
    public float Right;
}