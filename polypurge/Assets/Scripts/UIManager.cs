using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject pauseMenu;

    [SerializeField] private EventSystem eventSystem;

    public bool pauseState;

    private GameObject currentbutton;

    private void Update()
    {
        if (eventSystem != null)
        {
            
            if (eventSystem.currentSelectedGameObject != null)
            {
                currentbutton = eventSystem.currentSelectedGameObject;                
            }
            else
            {
                eventSystem.SetSelectedGameObject(currentbutton);
            }
        }
    }

    public void LoadScene(int level)
    {
        Debug.Log("Level loaded: " + level);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(level);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void DoPauze(InputAction.CallbackContext context)
    {
        if (pauseState)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }

    }
    public void ResumeGame()
    {
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
        pauseState = false;
    }

    public void PauseGame()
    {
        pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
        pauseState = true;
    }
}
