using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance { get; private set; }

    [SerializeField] private float healthDecreaseInterval = 1f;
    [SerializeField] private int healthDecreaseAmount = 5;
    [SerializeField] private Slider healthSlider;
    public int Health { get; private set; } = 500;
    public float CurrentTime { get; private set; }
    [SerializeField] private TextMeshProUGUI timerText;
    private float _currentTime = 0f;
    public bool IsTimerActive { get; private set; } = true;
    [SerializeField] private GameObject gameOverCanvas;
    private bool isGamePaused = false;
    private float healthDecreaseTimer = 0f;
    private void Awake()
    {
        gameOverCanvas?.SetActive(false);
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        InitializeHealthUI();
        gameOverCanvas?.SetActive(false);
        LockCursor();
    }
    private void Update()
    {
        if (Health > 0 && !isGamePaused)
        {
            HandleHealthDecrease();
            if (IsTimerActive)
            {
                UpdateTimer();
            }
        }
        CurrentTime += Time.deltaTime;
    }
    private void HandleHealthDecrease()
    {
        healthDecreaseTimer += Time.deltaTime;
        if (healthDecreaseTimer >= healthDecreaseInterval)
        {
            DecreaseHealth(healthDecreaseAmount);
            healthDecreaseTimer = 0f;
        }
    }
    private void UpdateTimer()
    {
        _currentTime += Time.deltaTime;
        if (timerText != null)
        {
            timerText.text = FormatTime(_currentTime);
        }
    }
    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return $"{minutes:00}:{seconds:00}";
    }
    private void InitializeHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = 500;
            healthSlider.minValue = 0;
            healthSlider.value = Health;
        }
    }
    private void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = Health;
        }
    }
    public void IncreaseHealth(int amount)
    {
        Health = Mathf.Min(Health + amount, 500);
        UpdateHealthUI();
    }
    public void DecreaseHealth(int amount)
    {
        Health = Mathf.Max(Health - amount, 0);
        UpdateHealthUI();
        if (Health <= 0)
        {
            PlayerDeath();
        }
    }
    private void PlayerDeath()
    {
        Time.timeScale = 0;
        gameOverCanvas?.SetActive(true);
        timerText.text = $" {FormatTime(_currentTime)} ";
        UnlockCursor();
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }
    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void RestartGame()
    { 
        Application.OpenURL(Application.absoluteURL);
    }
}
