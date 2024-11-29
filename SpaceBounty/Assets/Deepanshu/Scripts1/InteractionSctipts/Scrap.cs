using UnityEngine;
public class Scrap : InteractionFoundation
{
    public System.Action OnScrapCollected;
    enum ColorList
    {
        Red,
        Green,
    }
    [SerializeField] 
    private Renderer objectRenderer;
    [SerializeField]
    private Color[] colorList;
    [SerializeField] internal float minInterval = 1f;
    [SerializeField] internal float maxInterval = 3f;
    [SerializeField]
    private GameObject greenRespawnPrefab;
    public int damage = 15;
    private Material objectMaterial;
    private Color currentColor;
    private void Awake()
    {
        if (objectRenderer != null) return;

        if (TryGetComponent(out objectRenderer))
        {
            objectMaterial = objectRenderer.material;
        }
    }
    private void Start()
    {
        StartCoroutine(ChangeColorAtRandomIntervals());
    }
    private System.Collections.IEnumerator ChangeColorAtRandomIntervals()
    {
        while (true)
        {
            float waitTime = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(waitTime);
            currentColor = GetRandomColor();
            ChangeColor(currentColor);
        }
    }
    private void ChangeColor(Color color)
    {
        if (objectMaterial != null)
        {
            objectMaterial.color = color;
        }
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<Renderer>(out Renderer childRenderer))
            {
                Material childMaterial = childRenderer.material;
                if (childMaterial != null)
                {
                    childMaterial.color = color;
                }
            }
        }
    }
    private Color GetRandomColor()
    {
        int randomIndex = Random.Range(0, colorList.Length);
        return colorList[randomIndex];
    }
    public override void Interact(GameManager gameManager)
    {
        if (gameManager != null)
        {
            if (currentColor == colorList[(int)ColorList.Red])
            {
                gameManager.DecreaseHealth(damage);
            }
            else if (currentColor == colorList[(int)ColorList.Green])
            {
                Instantiate(greenRespawnPrefab, transform.position, Quaternion.identity);
            }
            
            OnScrapCollected?.Invoke();
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out var playerController))
        {
            Interact(GameManager.Instance);  
        }
    }
}
