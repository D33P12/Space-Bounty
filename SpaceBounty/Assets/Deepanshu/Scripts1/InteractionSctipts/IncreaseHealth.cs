using UnityEngine;

public class IncreaseHealth : MonoBehaviour
{
    public int healthIncreaseAmount = 10;
    public float lifetime = 5f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.IncreaseHealth(healthIncreaseAmount);

            Destroy(gameObject);
        }
    }
}
