using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
 private float healthDecreaseTimer = 0f; 
 
 [SerializeField] private float healthDecreaseInterval = 1f; 
 [SerializeField] private int healthDecreaseAmount = 5;
 [SerializeField] private Slider healthSlider;
 [SerializeField] private GameObject gameOverCanvas;
 [SerializeField] private GameObject pauseMenuCanvas;
 
 private bool isGamePaused = false;
 private void Awake()
 {
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
  if (healthSlider != null)
  {
   healthSlider.maxValue = 100; 
   healthSlider.minValue = 0;  
   healthSlider.value = Health; 
  }
  
  LockCursor();
  
  if (gameOverCanvas != null)
  {
   gameOverCanvas.SetActive(false); 
  }
  
  if (pauseMenuCanvas != null)
  {
   pauseMenuCanvas.SetActive(false);
  }
 }
 private void Update()
 {
  if (Health > 0)
  {
   healthDecreaseTimer += Time.deltaTime;
   if (healthDecreaseTimer >= healthDecreaseInterval)
   {
    DecreaseHealth(healthDecreaseAmount);
    healthDecreaseTimer = 0f; 
   }
  }
  if (Input.GetKeyDown(KeyCode.Escape) && (gameOverCanvas == null || !gameOverCanvas.activeSelf))
  {
   TogglePauseMenu();
  }
 }
 private void UpdateHealthUI()
 {
  if (healthSlider != null)
  {
   healthSlider.value = Health; 
  }
 }
 public static GameManager Instance { get; private set; }
 public int Health { get; private set; } = 100;

 public void IncreaseHealth(int amount)
 {
  Health += amount;
  UpdateHealthUI();
  if (Health > 100) Health = 100;
  Debug.Log($"Health: {Health}");
 }
 public void DecreaseHealth(int amount)
 {
  Health -= amount;
  if (Health < 0) Health = 0; 
  UpdateHealthUI();
  Debug.Log($"Health: {Health}");
  if (Health <= 0)
  {
   PlayerDeath();
  }
 }
 private void PlayerDeath()
 {
  Time.timeScale = 0;  
  if (gameOverCanvas != null)
  {
   gameOverCanvas.SetActive(true);
  }
  UnlockCursor();

 }
 public void RestartGame()
 {
  Health = 100;
  UpdateHealthUI();
  if (gameOverCanvas != null)
  {
   gameOverCanvas.SetActive(false);
  }
  Time.timeScale = 1;
  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
 }
 public void LoadMainMenu()
 {
  Time.timeScale = 1; 
  SceneManager.LoadScene(0); 
 }
 
 public void TogglePauseMenu()
 {
  if (gameOverCanvas != null && gameOverCanvas.activeSelf)
  {
   return;
  }
  isGamePaused = !isGamePaused;
  if (isGamePaused)
  {
   if (pauseMenuCanvas != null)
   {
    pauseMenuCanvas.SetActive(true);
   }
   UnlockCursor();
   Time.timeScale = 0;
  }
  else
  {
   if (pauseMenuCanvas != null)
   {
    pauseMenuCanvas.SetActive(false);
   }
   LockCursor();
   Time.timeScale = 1;
  }
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
}
