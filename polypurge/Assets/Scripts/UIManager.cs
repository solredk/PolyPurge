using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Canvas pauseMenu;
    public bool pauseState;
    public void LoadScene(int level)
    {
        Debug.Log("Level loaded: " + level);
        SceneManager.LoadScene(level);
    }

    public void QuitGame()
    {
        Debug.Log("Game is exiting...");
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
