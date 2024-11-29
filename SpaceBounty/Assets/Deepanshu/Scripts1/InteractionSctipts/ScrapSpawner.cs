using System.Collections.Generic;
using UnityEngine;
public class ScrapSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnableItems;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private int maxSpawnCount = 5;
    [SerializeField] private float minDistanceBetweenItems = 2f;
    [SerializeField] private NavScript navScript;
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
            return;
        }
        if (IsPositionClear(spawnPoint.position))
        {
            GameObject itemToSpawn = spawnableItems[Random.Range(0, spawnableItems.Count)];
            GameObject newItem = Instantiate(itemToSpawn, spawnPoint.position, Quaternion.identity);
            activeItems.Add(newItem);
            navScript?.RegisterPrefabSpawn(newItem);
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
        activeItems.Remove(item);
        navScript?.UnregisterPrefab(item);
        Destroy(item);
        SpawnItemAt(spawnPoint);
    }
}


