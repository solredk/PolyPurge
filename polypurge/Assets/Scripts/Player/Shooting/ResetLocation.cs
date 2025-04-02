using UnityEngine;

public class ResetLocation : MonoBehaviour
{
    [SerializeField] private Transform resetPosition;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = resetPosition.position;
        }
    }
}
