using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI intelText;


    public void UpdateObjectiveText(int count, int maxAmount, ObjectiveType objective)
    {
        Debug.Log("Objective: " + objective.ToString() + " " + count.ToString() + "/" + maxAmount.ToString());
        if (intelText == null)
        {
            return;
        }
        if (count == maxAmount)
        {
            intelText.text = "Objective Complete return to start";
        }
        else if (count < maxAmount )
        {
            if(objective == ObjectiveType.Kill)
            {
                intelText.text = "Kill " + count.ToString() + "/" + maxAmount.ToString();
            }
            else if (objective == ObjectiveType.Intel)
            {
                intelText.text = "Intel " + count.ToString() + "/" + maxAmount.ToString();
            }
        }
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
            ammoText.text =  currentAmmoInMag + "/" + remainingAmmo;
        }
    }
}
