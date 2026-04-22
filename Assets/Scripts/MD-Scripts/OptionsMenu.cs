using UnityEngine;
using UnityEngine.SceneManagement;


public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private int mainMenuSceneIndex = 0;
    [SerializeField] private int gameplaySceneIndex = 1;

    // Use this for the Options button opened from Main Menu.
    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(mainMenuSceneIndex);
    }

    // Use this for the Options button opened from Pause Menu.
    public void BackToPausedGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(gameplaySceneIndex);
    }
}
