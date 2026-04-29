using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class OptionsMenu : MonoBehaviour
{
    //Moises---------------
    [SerializeField] private Button musicVolumeButton;
    [SerializeField] private Button soundEffectVolumeButton;
    [SerializeField] private TextMeshProUGUI musicVolumeText;
    [SerializeField] private TextMeshProUGUI soundEffectVolumeText;
    [SerializeField] private int mainMenuSceneIndex = 0;
    [SerializeField] private int gameplaySceneIndex = 1;

    private void Awake()
    {
       // Check if buttons are assigned to avoid the error
        if (soundEffectVolumeButton != null) {
            soundEffectVolumeButton.onClick.AddListener(() => {
                SoundManager.Instance.ChangeVolume();
                UpdateVisual();
            });
        }

        if (musicVolumeButton != null) {
            musicVolumeButton.onClick.AddListener(() => {
                MusicManager.Instance.ChangeVolume();
                UpdateVisual();
            });
        }

        //Update the visual of the volume buttons
        UpdateVisual();
    }

    private void Start()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        soundEffectVolumeText.text = "Sound FX: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicVolumeText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);
    }
    //----------------------
    
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
