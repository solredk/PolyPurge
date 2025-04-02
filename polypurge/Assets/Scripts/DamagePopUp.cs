using TMPro;
using UnityEngine;

public class DamagePopUp : MonoBehaviour
{
    public static DamagePopUp current;
    public GameObject prefab;

    private void Awake()
    {
        if (current == null)
            current = this;
        else
            Destroy(gameObject);
    }

    public void CreatePopUp(Vector3 position, string text)
    {
        if (prefab == null)
        {
            Debug.LogError("DamagePopUp prefab is not assigned!");
            return;
        }

        GameObject damagePopUp = Instantiate(prefab, position, Quaternion.identity);
        TextMeshProUGUI temp = damagePopUp.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        if (temp != null)
        {
            temp.text = text;
        }

        Destroy(damagePopUp, 1f);
    }
}
