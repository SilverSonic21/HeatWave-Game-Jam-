using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{
    public GameObject credits;
    public void OnPlayAgainButtonClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void OnMainMenuButton()
    {
    SceneManager.LoadSceneAsync("TitleScreen");    
    }

    public void Play()
    {
        SceneManager.LoadSceneAsync("MainLevel");
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
public void TurnOnCredits()
    {
        credits.SetActive(true);
    }

    public void TurnOffCredits()
    {
        credits.SetActive(false);
    }
}
