using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int intelCount;
    
    public void AddIntel(int amount)
    {
        intelCount += amount;
    }

}
