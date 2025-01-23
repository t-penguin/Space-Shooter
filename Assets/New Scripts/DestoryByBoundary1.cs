using UnityEngine;

public class DestoryByBoundary1 : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}