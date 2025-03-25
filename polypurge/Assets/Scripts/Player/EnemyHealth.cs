using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField] private ParticleSystem explosionEffect;
    [SerializeField] private bool isExplosive;
    [SerializeField] private float explosionDamage = 10f;
    [SerializeField] private Collider explosionTrigger; // Trigger collider die de explosierange bepaalt
    [SerializeField] private GameObject destroyedPrefab;
    [SerializeField] private float destroyDelay = 2f; // Zorg dat effecten afspelen voordat het object verdwijnt

    private bool isDead = false;
    private PlayerHealth playerHealth;

    private void Explode()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        if (destroyedPrefab != null)
        {
            Instantiate(destroyedPrefab, transform.position, transform.rotation);
        }

        Destroy(gameObject, destroyDelay); // Zorg dat het object pas na een paar seconden verdwijnt
    }

    private void Update()
    {
        if (!isDead && hitpoints <= 0)
        {
            isDead = true;
            
            if (isExplosive)
            {
                if (isDead && isExplosive)
                {
                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(explosionDamage);
                    }
                    Explode();
                }
            }
            else
            {
                Explode(); // Direct exploderen als het geen explosieve vijand is
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerHealth>() != null)
        {
            playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerHealth>() != null)
        {
            playerHealth = null;
        }
    }
}
