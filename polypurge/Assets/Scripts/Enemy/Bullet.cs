using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    private void FixedUpdate()
    {
        transform.position += transform.forward * Time.fixedDeltaTime * speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        Transform hitTransform = other.transform;

        
        if (hitTransform.CompareTag("Player"))
        {
            PlayerHealth playerHealth = hitTransform.GetComponent<PlayerHealth>();

            //check if the player health is not null and than take damage
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10f);
            }

        }
        // Destroy the bullet on collision with any object
        Destroy(gameObject);

    }
}
