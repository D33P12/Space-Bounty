using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
 private float healthDecreaseTimer = 0f; 
 [SerializeField] private float healthDecreaseInterval = 1f; 
 [SerializeField] private int healthDecreaseAmount = 5;
 [SerializeField] private Slider healthSlider;
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
  Cursor.lockState = CursorLockMode.Confined;
  Cursor.visible = false;
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
  Debug.Log("Player has died.");
 }
}
