using UnityEngine;

public class ModifierItem : MonoBehaviour
{
    [SerializeField] private ModifierType _type;
    [SerializeField] private float _modifierValue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;

        GameEvents.PickUpModifier(_type, _modifierValue);
        Destroy(gameObject);
    }
}