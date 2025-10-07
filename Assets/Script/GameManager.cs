using UnityEngine;
using UnityEngine.SceneManagement; 
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("遊戲狀態")]
    public bool isGameActive = true;

    [Header("計時器設定")]
    public float timeRemaining = 60f;
    public TextMeshProUGUI timerText;

    [Header("UI 面板")]
    public GameObject gameOverPanel;
    
    [Header("玩家物件")]
    public PlayerController playerController;

    [Header("燈光設定")]
    public Light mainLight;
    public Slider brightnessSlider;

    void Start()
    {
        if (mainLight != null && brightnessSlider != null)
        {
            brightnessSlider.value = mainLight.intensity;
            brightnessSlider.onValueChanged.AddListener(AdjustBrightness);
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TogglePauseAndUI();
        }

        if (isGameActive)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                UpdateTimerDisplay(timeRemaining);
                GameOver();
            }
        }
    }

    public void TogglePauseAndUI()
    {
        if (!gameOverPanel.activeSelf && !(FindFirstObjectByType<GoalTrigger>() != null && FindFirstObjectByType<GoalTrigger>().victoryUIPanel.activeSelf))
        {
             isGameActive = !isGameActive; 

            if (isGameActive)
            {
                gmode();
            }
            else
            {
                UImode();
            }
        }
    }

    void gmode()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if(playerController != null) playerController.enabled = true;
    }

    void UImode()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if(playerController != null) playerController.enabled = false;
    }

    public void GameOver()
    {
        isGameActive = false;
        Debug.Log("遊戲失敗！");
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        UImode(); 
    }
    

    public void AdjustBrightness(float value)
    {
        if (mainLight != null)
        {
            mainLight.intensity = value;
        }
    }

    void UpdateTimerDisplay(float timeToDisplay)
    {
        if (timerText == null) return;
        if (timeToDisplay < 0) timeToDisplay = 0;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }


    public void RestartGame()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
