using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    private const string HighScoreKey = "HighScore";

    [SerializeField] private float startTimeSeconds = 60f;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private string timerPrefix = "TIME: ";

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject[] filledStars;
    [SerializeField] private int pointsToPass = 100;

    [SerializeField] private GameObject winTitleImage;
    [SerializeField] private GameObject gameOverTitleImage;
    [SerializeField] private TMP_Text gameOverScoreText;
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private string gameOverScorePrefix = "YOUR SCORE: ";
    [SerializeField] private string highScorePrefix = "HIGH SCORE: ";

    private float _timeRemaining;
    private bool _hasEnded;

    private void Start()
    {
        // Ensure gameplay starts unpaused when scene loads.
        Time.timeScale = 1f;
        _timeRemaining = Mathf.Max(0f, startTimeSeconds);

        //Make sure the game over screen is not active
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }

        UpdateGameOverScoreText(startingScore: true, finalScore: 0);
        UpdateTimerText();
    }

    private void Update()
    {
        if (_hasEnded)
        {
            return;
        }

        _timeRemaining -= Time.deltaTime;
        if (_timeRemaining <= 0f)
        {
            _timeRemaining = 0f;
            _hasEnded = true;
            UpdateTimerText();
            Time.timeScale = 0f;
            GameOver();
            return;
        }

        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        if (timerText == null)
        {
            return;
        }

        int wholeSeconds = Mathf.CeilToInt(_timeRemaining);
        timerText.text = timerPrefix + wholeSeconds;
    }

    private void GameOver(){
        Time.timeScale = 0f;
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }

        ScoreSystem scoreSystem = ScoreSystem.Instance != null ? ScoreSystem.Instance : FindObjectOfType<ScoreSystem>();
        int finalScore = scoreSystem != null ? scoreSystem.GetScore() : 0;
        UpdateGameOverScoreText(startingScore: false, finalScore: finalScore);

        // Reset filled star overlays so only earned stars are shown.
        if (filledStars != null)
        {
            foreach (GameObject star in filledStars)
            {
                if (star != null)
                {
                    star.SetActive(false);
                }
            }
        }

        bool isWin = finalScore >= pointsToPass;

        if (isWin){
            // WIN STATE
            if (winTitleImage != null) winTitleImage.SetActive(true);
            if (gameOverTitleImage != null) gameOverTitleImage.SetActive(false);

            AwardStar(finalScore);
        }else{
            // LOSE STATE
            if (winTitleImage != null) winTitleImage.SetActive(false);
            if (gameOverTitleImage != null) gameOverTitleImage.SetActive(true);
        }
    }

    private void AwardStar(int score){
        if (filledStars == null || filledStars.Length == 0)
        {
            return;
        }

        if (filledStars.Length > 0 && filledStars[0] != null && score >= pointsToPass)
        {
            filledStars[0].SetActive(true);
        }

        if (filledStars.Length > 1 && filledStars[1] != null && score >= pointsToPass * 2)
        {
            filledStars[1].SetActive(true);
        }

        if (filledStars.Length > 2 && filledStars[2] != null && score >= pointsToPass * 3)
        {
            filledStars[2].SetActive(true);
        }
    }

    private void UpdateGameOverScoreText(bool startingScore, int finalScore)
    {
        int displayedScore = startingScore ? 0 : finalScore;

        int savedHighScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        int updatedHighScore = Mathf.Max(savedHighScore, displayedScore);

        if (!startingScore && updatedHighScore > savedHighScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, updatedHighScore);
            PlayerPrefs.Save();
        }

        if (gameOverScoreText != null)
        {
            gameOverScoreText.text = gameOverScorePrefix + displayedScore.ToString("D5");
        }

        if (highScoreText != null)
        {
            highScoreText.text = highScorePrefix + updatedHighScore.ToString("D5");
        }
    }

}

