using UnityEngine;

public class BGScroller1 : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed;
    [SerializeField] private float _tileSizeZ;

    private Vector3 _startPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float positionChangeZ = Mathf.Repeat(Time.time * _scrollSpeed, _tileSizeZ);
        transform.position = _startPosition + Vector3.back * positionChangeZ;
    }
}