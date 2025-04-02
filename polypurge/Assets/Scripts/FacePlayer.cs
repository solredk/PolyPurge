using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (player != null)
        {
            transform.LookAt(player.transform);
        }
    }
}
