using UnityEngine;
public class GameManager : MonoBehaviour
{
 private int totalScrap = 0;
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
  Cursor.lockState = CursorLockMode.Confined;
  Cursor.visible = false;
 }
 public static GameManager Instance { get; private set; }
 public int Health { get; private set; } = 100;

 public void IncreaseHealth(int amount)
 {
  Health += amount;
  Debug.Log($"Health: {Health}");
 }
 public void DecreaseHealth(int amount)
 {
  Health -= amount;
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
 public void AddScrap(int amount)
 {
  totalScrap += amount;
  
 }
 
}
