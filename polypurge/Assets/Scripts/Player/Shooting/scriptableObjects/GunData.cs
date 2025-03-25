using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]
public class GunData : ScriptableObject
{
    [Header("Gun Stats")]
    public string gunName;
    public float shootRange = 100f;
    public float fireRate = 0.1f;
    public float reloadTime = 2f;
    public int maxAmmo = 90;
    public int maxAmmoInMag = 30;
    public int damage = 10;
}
