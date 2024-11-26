using UnityEngine;

public class AllManagerScript : MonoBehaviour
{
  static AllManagerScript _instance;

  void Awake()
  {
    if (_instance != null && _instance != this)
    {
      Destroy(this.gameObject);
    }
    else
    {
      _instance = this;
      DontDestroyOnLoad(this.gameObject);
    }
  }
}
