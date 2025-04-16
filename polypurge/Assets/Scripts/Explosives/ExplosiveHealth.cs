using UnityEngine;

public class ExplosiveHealth : Health
{
    [Header("Explosion Settings")]
    [SerializeField] private ParticleSystem explosionEffect;
    [SerializeField] private GameObject destroyedPrefab;

    [SerializeField] private float explosionDamage = 10f;
    [SerializeField] private float destroyDelay = 2f;

    private PlayerHealth playerHealth;
    private bool isDead = false;

    private void Update()
    {
        if (!isDead && hitpoints <= 0)
        {
            isDead = true;

            if ( playerHealth != null)
            {
                playerHealth.TakeDamage(explosionDamage);
            }

            Explode();
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        DamagePopUp.current.CreatePopUp(transform.position, damage.ToString());
    }

    private void Explode()
    {
        if (explosionEffect != null)
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        if (destroyedPrefab != null)
            Instantiate(destroyedPrefab, transform.position, transform.rotation);

        Destroy(gameObject, destroyDelay);
    }

    private void OnTriggerEnter(Collider other)
    {
        playerHealth = other.GetComponent<PlayerHealth>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerHealth>() == playerHealth)
            playerHealth = null;
    }
}