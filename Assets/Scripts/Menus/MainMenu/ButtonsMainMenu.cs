using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsMainMenu : MonoBehaviour
{
    public GameObject _menuMain;
    public GameObject _menuCredits;

    public void ButtonStart()
    {
        SceneManager.LoadScene("Game");
    }

    public void ButtonQuit()
    {
        Application.Quit();
    }

    public void ButtonCredit()
    {
        _menuMain.SetActive(false);
        _menuCredits.SetActive(true);
    }

    public void ButtonReturn()
    {
        _menuCredits.SetActive(false);
        _menuMain.SetActive(true);
    }
}
