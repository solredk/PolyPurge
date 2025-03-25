using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("playerstats")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float jumpHeight = 3f;




    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;


    private bool isSprinting = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void ProcessMove(Vector2 input)
    {
        float currentSpeed = isSprinting ? sprintSpeed : walkSpeed; // Gebruik de juiste snelheid

        Vector3 moveDirection = new Vector3(input.x, 0, input.y);
        controller.Move(transform.TransformDirection(moveDirection) * currentSpeed * Time.deltaTime);

        // Zwaartekracht toepassen
        playerVelocity.y += gravity * Time.deltaTime;

        // Reset val snelheid als de speler op de grond is
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;

        controller.Move(playerVelocity * Time.deltaTime);

        isGrounded = controller.isGrounded;
    }

    public void SetSprint(bool sprinting)
    {
        isSprinting = sprinting;
    }
}
