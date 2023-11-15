using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonGameMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    public void ButtonResume()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
    }

    public void ButtonRetry()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ButtonExit()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

    public void ButtonPause()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ButtonPause();
        }
    }
}
