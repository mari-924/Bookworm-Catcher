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

    private void Awake()
    {
        soundEffectVolumeButton.onClick.AddListener(() => {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicVolumeButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
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

    
    //Exits the options menu and goes back to the main menu
   public void BackToMainMenu(){
    SceneManager.LoadSceneAsync(0);
   }
}
