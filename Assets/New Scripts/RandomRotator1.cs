using UnityEngine;

public class RandomRotator1 : MonoBehaviour
{
    [SerializeField] private float _tumble;
    private void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * _tumble;
    }
}