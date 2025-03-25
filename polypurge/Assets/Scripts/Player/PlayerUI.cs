using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI promptText;
    [SerializeField] TextMeshProUGUI ammoText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePromptText(string promptMassage)
    {
        promptText.text = promptMassage;
    }

    public void UpdateAmmoText(int currentAmmoInMag, int remainingAmmo, bool reloading)
    {
        if (reloading)
        {
            // Toon de puntjes die zich dynamisch aanpassen
            ammoText.text = "Reloading" + new string('.', (int)(Time.time % 3 + 1));
        }
        else
        {
            // Normale ammo display
            ammoText.text = $"Ammo: {currentAmmoInMag} / {remainingAmmo}";
        }
    }
}
