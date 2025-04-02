using UnityEngine;


    public enum ObjectiveType
    {
        None,
        Intel,
        Kill
    }

public class GameManager : MonoBehaviour
{
    [SerializeField] private int intelCount;
    [SerializeField] private int maxIntel;

    [SerializeField] private int killCount;
    [SerializeField] private int maxKills;

    [SerializeField] private GameObject exitDoor;

    [SerializeField] private PlayerUI playerUI;

    public ObjectiveType objective;

    private void Awake()
    {
        if(objective != ObjectiveType.None)
        {
            UpdateObjectiveUI();
        }
        // Alleen bij een geldige objective updaten
        if (objective != ObjectiveType.None)
        {
            UpdateObjectiveUI();
        }
    }

    private void Update()
    {
        if (ObjectiveComplete())
        {
            CanExit();
        }

    }

    public void AddIntel(int amount)
    {
        intelCount += amount;
        UpdateObjectiveUI();
    }
    public void AddKill(int amount)
    {
        killCount += amount;
        UpdateObjectiveUI();
    }


    private void UpdateObjectiveUI()
    {
        switch (objective)
        {
            case ObjectiveType.Intel:
                playerUI.UpdateObjectiveText(intelCount, maxIntel, objective);
                break;
            case ObjectiveType.Kill:
                playerUI.UpdateObjectiveText(killCount, maxKills, objective);
                break;
        }
    }

    public void CanExit()
    {

        UpdateObjectiveUI();
    }




    private bool ObjectiveComplete()
    {
        if (intelCount >= maxIntel && objective != ObjectiveType.None)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
