using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonGameMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    public void ButtonResume()
    {
        pauseMenu.SetActive(false);
    }

    public void ButtonRetry()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ButtonExit()
    {
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
