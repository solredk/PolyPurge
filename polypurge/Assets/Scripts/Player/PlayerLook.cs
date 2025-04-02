using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Header("mouse sensetivity")]
    
    [SerializeField] private float sensX = 100f;
    [SerializeField] private float sensY = 100f;

    [SerializeField] private Camera cam;

    private float mouseX;
    private float mouseY;

    private float multiplier = 0.01f;

    private float xRotation;
    private float yRotation;

    [Header("interacting")]
    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private LayerMask interactLayer;

    private bool interacted;

    private PlayerUI playerUI;

    [SerializeField] private UIManager uiManager;

    private Interactable interactable;

    private void Start()
    {
        // Get the camera component from the player gameobject child
        cam = GetComponentInChildren<Camera>();
        playerUI = GetComponent<PlayerUI>();
        if (!uiManager.pauseState)
        {
            //make you unable to see the cursor and lock it in the center of the screen
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (uiManager.pauseState)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }


    }

    private void Update()
    {
        playerUI.UpdatePromptText(string.Empty);

        //rotate the camera and the player
        if (cam != null && !uiManager.pauseState)
        {
            cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            transform.rotation = Quaternion.Euler(0, yRotation, 0);
        }

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hitInfo;

        //create an raycast to check if the player is looking at an interactable object
        if (Physics.Raycast(ray, out hitInfo, interactDistance, interactLayer))
        {
            //check if the object has an interactable component
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                //if it does put it in an variable
                interactable = hitInfo.collider.GetComponent<Interactable>();

                //get the prompotmassage from the interactable script and show it on the screen
                playerUI.UpdatePromptText(hitInfo.collider.GetComponent<Interactable>().promptMassage);
                //if you have interacted with the object call the interact function and put interacted into false again
                if (interacted)
                {
                    interactable.BaseInteract();
                    interacted = false;
                }
            }

        }
    }

    public void Interacting()
    {
        //if called upon you have interacted with the object
        interacted = true;

    }

    public void MyInput(Vector2 input)
    {
        if (!uiManager.pauseState)
        {
            //putting the mouse movements into variables
            mouseX = input.x;
            mouseY = input.y;

            //rotate the camera and the player
            yRotation += mouseX * sensX * multiplier;
            xRotation -= mouseY * sensY * multiplier;

            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        }
    }
}