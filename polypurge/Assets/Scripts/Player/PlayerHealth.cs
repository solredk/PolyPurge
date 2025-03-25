using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    [Header("health bar display")]
    [SerializeField] private float lerpTimer;
    [SerializeField] private float chipSpeed = 2f;

    [SerializeField] private Image frontHealthBar;
    [SerializeField] private Image backHealthBar;


    [Header("Damage overlay")]
    [SerializeField] private Image overlay;

    [SerializeField] private float duration;
    [SerializeField] private float fadeSpeed;

    private float durationTimer;


    private void Start()
    {
        //make sure you arent able to see the overlay
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            HealDamage(10);
        }

        UpdateHealthUI();

        //checking if the overlay is visible
        if (overlay.color.a > 0)
        {
            //if the hitpoints are lower than 30 it should be visible
            if (hitpoints < 30)
                return;
            //if the hitpoints are higher than 30 it should fade out slowly
            durationTimer += Time.deltaTime;
            //when the the duration time is done the overlay should fade out
            if (durationTimer > duration)
            {
                //putting the alpha of the overlay in a variable
                float tempAlpha = overlay.color.a;
                //slowly fade out
                tempAlpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }
        }
    }

    public override void TakeDamage(float damage)
    {
        //the base take damage function
        base.TakeDamage(damage);

        //reset the lerp timer
        lerpTimer = 0;

        //update the health UI
        UpdateHealthUI();
        durationTimer = 0;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);
    }
    public override void HealDamage(float heal)
    {
        base.HealDamage(heal);
        lerpTimer = 0;
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        //putting the front and back fill amount in a variable
        float fillFront = frontHealthBar.fillAmount;
        float fillBack = backHealthBar.fillAmount;
        //calculating the fraction of the hitpoints
        float hFraction = hitpoints / maxhitpoints;
        //lerping the fill amount of the front and back health bar
        lerpTimer += Time.unscaledDeltaTime;

        //squaring the percentage complete to make the lerp more smooth
        float percentageComplete = Mathf.Clamp01(lerpTimer / chipSpeed);
        percentageComplete *= percentageComplete;

        //if the fill amount of the front health bar is bigger than the fraction of the hitpoints so you take damage
        if (fillBack > hFraction) 
        {
            //set the fill amount of the front health bar to the fraction of the hitpoints
            frontHealthBar.fillAmount = hFraction;
            //lerp the fill amount of the back health bar to the fraction of the hitpoints to make sure it goes smooth
            backHealthBar.fillAmount = Mathf.Lerp(fillBack, hFraction, percentageComplete);
            //set the color of the back health bar to red so you are able to see that you are taking damage
            backHealthBar.color = Color.red;
        }
        //if the fill amount of the front health bar is smaller than the fraction of the hitpoints so you heal
        else if (fillFront < hFraction) 
        {
            backHealthBar.fillAmount = hFraction;
            frontHealthBar.fillAmount = Mathf.Lerp(fillFront, backHealthBar.fillAmount, percentageComplete);
            backHealthBar.color = Color.green;
        }
    }
}