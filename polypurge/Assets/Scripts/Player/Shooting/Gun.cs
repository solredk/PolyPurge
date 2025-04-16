using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [Header("Gun Data")]
    [SerializeField] private GunData gunData;

    [Header("Effects")]
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject bulletImpactPrefab;
    

    [Header("Crosshair")]
    [SerializeField] private Camera playerCamera;

    [SerializeField] private bool startWithAmmo = true;

    public PlayerUI playerUI;

    private int currentAmmoInMag;
    private int remainingAmmo;

    private float lastShotTime = 0f;

    private bool isReloading = false;
    private bool isShooting = false;

    void Start()
    {
        playerUI = GetComponentInParent<PlayerUI>();

        // if you dont have a gunData, return
        if (gunData == null) 
            return;
        if (startWithAmmo)
        {
            // set the values from the gundata
            currentAmmoInMag = gunData.maxAmmoInMag;
            remainingAmmo = gunData.maxAmmo - gunData.maxAmmoInMag;
        }
        //update the ammo display
        UpdateAmmoDisplay();
    }

    private void FixedUpdate()
    {
        // if the shooting bool is true you can shoot
        if (isShooting)
        {
            Shooting();
        }
    }

    public void DoShoot(InputAction.CallbackContext context)
    {
        // Checking if you are shooting if shooting bool is true else when cancelt de bool is false
        if (context.performed) 
            isShooting = true;
        if (context.canceled) 
            isShooting = false;
    }

    private void Shooting()
    {
        // if you are reloading or the time between the last shot and the current time is less than the fire rate than you can't shoot
        if (isReloading || Time.time - lastShotTime < gunData.fireRate) return;

        // checking if you have any ammo left in the mag
        if (currentAmmoInMag > 0)
        {
            
            currentAmmoInMag--;
            lastShotTime = Time.time;

            //setting the raycast to the middle of the camera screen
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            //setting the layermask to the layers you want to hit
            int layerMask = LayerMask.GetMask("Default", "Enemy", "Environment");

            //if the raycast hits something and ignores the trigger colliders
            if (Physics.Raycast(ray, out RaycastHit hit, gunData.shootRange, layerMask, QueryTriggerInteraction.Ignore))
            {
                //if you got an bulletimpact prefab
                if (bulletImpactPrefab != null)
                {
                    //setting the hit point to 0.01f in front of the hit point
                    Vector3 impactPosition = hit.point + hit.normal * 0.01f;
                    //instantiat the bulletimpact prefab
                    GameObject impact = Instantiate(bulletImpactPrefab, impactPosition, Quaternion.LookRotation(hit.normal),hit.transform);
                    impact.transform.localScale = Vector3.one;  // Reset de schaal
                    //destroy the impact after 2 seconds
                    Destroy(impact, 2f);
                }
                //if the hit collider has an enemyhealth script than the object takes damage
                EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(gunData.damage);
                }
            }

            if (muzzleFlash != null)
            {
                //the muzzefplash effect will play
                muzzleFlash.Play();
            }
            //update the ammo display
            UpdateAmmoDisplay();
        }
        else
        {
            Debug.Log("Out of ammo!");
        }
    }

    public void DoReload(InputAction.CallbackContext context)
    {
        //checking if you can reload if you cant retun
        if (isReloading || currentAmmoInMag == gunData.maxAmmoInMag || remainingAmmo <= 0) return;
        //if you can return start the reload coroutine
        StartCoroutine(Reload());
    }

    public void AddAmmo(int ammoAmount)
    {
        //add the ammo to the remaining ammo
        remainingAmmo += ammoAmount;
        //update the ammo display
        UpdateAmmoDisplay();
    }

    private IEnumerator Reload()
    {
        // when you dont have ammo left return
        if (remainingAmmo <= 0) yield break;

        // set the isReloading bool to true
        isReloading = true;

        // start the reloading dots coroutine
        StartCoroutine(ShowReloadingDots());

        // wait until done with reloading
        yield return new WaitForSeconds(gunData.reloadTime);

        // calculate the ammo needed to fill the mag
        int neededAmmo = gunData.maxAmmoInMag - currentAmmoInMag;
        int ammoToLoad = Mathf.Min(neededAmmo, remainingAmmo);

        // fil the needed ammo in the mag and remove it from the remaining ammo
        currentAmmoInMag += ammoToLoad;
        remainingAmmo -= ammoToLoad;

        // reset the reloading bool
        isReloading = false;

        // Update the display
        UpdateAmmoDisplay();
    }

    private IEnumerator ShowReloadingDots()
    {
        // start with 1 dot
        int dotCount = 1;

        // stay as long as you are reloading
        while (isReloading)
        {
            // Update the ui to show the dots
            UpdateAmmoDisplay();

            // Verhoog het aantal puntjes
            dotCount++;
            if (dotCount > 3) dotCount = 1;

            // Wacht een korte tijd voordat we de tekst weer bijwerken
            yield return new WaitForSeconds(0.5f);
        }
    }

    void UpdateAmmoDisplay()
    {
        //update the ammo display
        playerUI.UpdateAmmoText(currentAmmoInMag, remainingAmmo, isReloading);

    }
}