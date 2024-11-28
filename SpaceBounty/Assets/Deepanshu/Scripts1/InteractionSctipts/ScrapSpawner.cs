using System.Collections.Generic;
using UnityEngine;

public class ScrapSpawner : MonoBehaviour
{
  [SerializeField]
    private List<GameObject> spawnableItems;
    [SerializeField]
    private List<Transform> spawnPoints;
    [SerializeField]
    private int maxSpawnCount = 5; 
    [SerializeField]
    private float minDistanceBetweenItems = 2f; 

    private List<GameObject> activeItems = new List<GameObject>(); 
    private void Start()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            SpawnItemAt(spawnPoint);
        }
    }
    public void SpawnItemAt(Transform spawnPoint)
    {
        if (activeItems.Count >= maxSpawnCount)
        {
            Debug.Log("Max spawn count reached. Skipping spawn.");
            return;
        }
        if (IsPositionClear(spawnPoint.position))
        {
            GameObject itemToSpawn = spawnableItems[Random.Range(0, spawnableItems.Count)];
            GameObject newItem = Instantiate(itemToSpawn, spawnPoint.position, Quaternion.identity);

            activeItems.Add(newItem);
            Debug.Log("Spawned new item: " + newItem.name);

            Scrap scrap = newItem.GetComponent<Scrap>();
            if (scrap != null)
            {
                scrap.OnScrapCollected += () => HandleItemDestroyed(newItem, spawnPoint);
            }
        }
    }
    private bool IsPositionClear(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, minDistanceBetweenItems);
        foreach (var collider in colliders)
        {
            if (collider.GetComponent<Scrap>() != null)
            {
                return false; 
            }
        }
        return true; 
    }
    private void HandleItemDestroyed(GameObject item, Transform spawnPoint)
    {
        Debug.Log("Item destroyed: " + item.name);
        activeItems.Remove(item);
        Destroy(item);
        SpawnItemAt(spawnPoint);
    }
}


