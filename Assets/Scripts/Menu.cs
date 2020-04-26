using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Main");
    }

    public void Back()
    {
        SceneManager.LoadScene("Intro");
    }

    public void Controls()
    {
        SceneManager.LoadScene("Controls");
    }

    public void PlayAgain()
    {
        GameManager.scoreValue = 0;
        SceneManager.LoadScene("Intro");
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}