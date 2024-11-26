using UnityEngine;
public class SampleColorScript : InteractionFoundation
{ 
    enum ColorList
    {
        Red,
        Green,
    }
    [SerializeField]
    private Renderer objectRenderer;
    [SerializeField]
    private Color[] colorList;
    [SerializeField]
    private float minInterval = 1f;
    [SerializeField]
    private float maxInterval = 3f;
    public int healthIncrease = 10;
    public int damage = 15;
    private Material objectMaterial;
    private Color currentColor;
    private void Awake()
    {
        if (objectRenderer == null)
        {
            objectRenderer = GetComponent<Renderer>();
        }

        if (objectRenderer != null)
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
    }
    private Color GetRandomColor()
    {
        int randomIndex = Random.Range(0, colorList.Length);
        return colorList[randomIndex];
    }
    public override void Interact(GameManager gameManager)
    {
        if (currentColor == colorList[(int)ColorList.Red]) 
        {
            gameManager.DecreaseHealth(damage); 
        }
        else if (currentColor == colorList[(int)ColorList.Green]) 
        {
            gameManager.IncreaseHealth(healthIncrease); 
        }
        
    }
}
