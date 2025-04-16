using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;


public class EnemyHealth : Health
{
    [SerializeField] private GameManager gameManager;
    
    [SerializeField] private GameObject deathEffect;

    [SerializeField] private Renderer bodyRenderer;
    [SerializeField] private EnemyBase enemyBase;

    private bool isDead = false;


    private void Update()
    {
        if (!isDead && hitpoints <= 0)
        {
            if (gameManager.objective == ObjectiveType.Kill)
            {
                gameManager.AddKill(1);
            }

            Destroy(gameObject);
            if (deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
            }
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        enemyBase.AlertAndAttackPlayer();
        StartCoroutine(HitEffect());
    }
    IEnumerator HitEffect()
    {
        Color originalColor = bodyRenderer.material.color;

        bodyRenderer.material.color = Color.red;
        yield return new WaitForSeconds(.2f);
        bodyRenderer.material.color = originalColor;
    }


}