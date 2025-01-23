using UnityEngine;

public class Mover1 : MonoBehaviour
{
    [SerializeField] private float _minSpeed = 1f;
    [SerializeField] private float _maxSpeed = 1f;

    public float Speed { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Speed = Random.Range(_minSpeed, _maxSpeed);
        GetComponent<Rigidbody>().linearVelocity = transform.forward * Speed;
    }

    public void SetSpeed(float min, float max)
    {
        _minSpeed = min;
        _maxSpeed = max;
        Speed = Random.Range(_minSpeed, _maxSpeed);
        GetComponent<Rigidbody>().linearVelocity = transform.forward * Speed;
    }
}