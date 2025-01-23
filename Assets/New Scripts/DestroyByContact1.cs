using UnityEngine;

public class DestroyByContact1 : MonoBehaviour
{
    [SerializeField] private GameObject _explosion;

    private void OnTriggerEnter(Collider other)
    {
        // These collisions don't constitute destruction
        bool boundaryCollision = other.CompareTag("Boundary");
        bool sameTypeCollision = other.CompareTag(tag);
        bool modifierCollision = other.CompareTag("Modifier");
        bool untaggedCollision = other.CompareTag("Untagged");
        if (boundaryCollision || sameTypeCollision || modifierCollision || untaggedCollision)
            return;

        // The player should not be destroyed when their barrier is up
        bool isPlayer = CompareTag("Player");
        if (isPlayer)
        {
            if (GetComponent<PlayerController1>().IsShielded)
                return;
        }

        // Asteroid destruction triggers an event
        bool isAsteroid = CompareTag("Asteroid");
        bool barrierHit = other.CompareTag("Barrier");
        bool shieldedHit = other.CompareTag("Player") && other.GetComponent<PlayerController1>().IsShielded;
        if (isAsteroid)
        {
            Debug.Log($"Asteroid is colliding with {other.name} tagged with {other.tag}");
            if (barrierHit)
            {
                // An asteroid colliding with a shielded player should damage the barrier
                float speed = GetComponent<Mover1>().Speed;
                GameEvents.HitShield((int)(speed * 2));
            }
            else
                GameEvents.DestroyAsteroid();
        }

        // Piercing bolts should not be destroyed on collision
        // The player should not be destroyed by this object if their barrier is up
        bool isPiercingBolt = other.CompareTag("Piercing");
        if (!isPiercingBolt && !barrierHit && !shieldedHit)
            Destroy(other.gameObject);

        // Explosion VFX
        Instantiate(_explosion, transform.position, transform.rotation);

        // Finally, destroy this game object
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (CompareTag("Player"))
        {
            GameEvents.DestroyPlayer();
            return;
        }
    }
}