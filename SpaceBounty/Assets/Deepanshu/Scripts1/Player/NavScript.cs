using System.Collections.Generic;
using UnityEngine;
public class NavScript : MonoBehaviour
{
    [SerializeField] private Transform player; 
    [SerializeField] private Transform firstArrow; 
    [SerializeField] private Transform secondArrow; 
    [SerializeField] private float maxDistance = 100f; 
    [SerializeField] private float rotationSpeed = 5f; 
    [SerializeField] private float switchDistanceThreshold = 10f; 
    private GameObject _closestScrap; 
    private float closestDistance; 
    private List<GameObject> spawnedScraps = new List<GameObject>();
    private void Update()
    {
        if (player == null || firstArrow == null || secondArrow == null) return;
        FindClosestScrap();
        UpdateArrowGuide(firstArrow);
        UpdateArrowGuide(secondArrow);
    }
    private void FindClosestScrap()
    {
        if (_closestScrap != null && closestDistance <= maxDistance)
        {
            foreach (GameObject scrap in spawnedScraps)
            {
                if (scrap == null || !scrap.activeInHierarchy) continue;

                float distanceToScrap = Vector3.Distance(player.position, scrap.transform.position);
                if (distanceToScrap < closestDistance && distanceToScrap > switchDistanceThreshold)
                {
                    closestDistance = distanceToScrap;
                    _closestScrap = scrap;
                }
            }
        }
        else
        {
            closestDistance = Mathf.Infinity;
            _closestScrap = null;

            foreach (GameObject scrap in spawnedScraps)
            {
                if (scrap == null || !scrap.activeInHierarchy) continue;

                float distanceToScrap = Vector3.Distance(player.position, scrap.transform.position);

                if (distanceToScrap < closestDistance)
                {
                    closestDistance = distanceToScrap;
                    _closestScrap = scrap;
                }
            }
        }
    }
    private void UpdateArrowGuide(Transform arrow)
    {
        if (_closestScrap != null && closestDistance <= maxDistance)
        {
            if (!arrow.gameObject.activeSelf)
                arrow.gameObject.SetActive(true);

            Vector3 directionToTarget = (_closestScrap.transform.position - player.position).normalized;

            directionToTarget.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            if (arrow == firstArrow)
            {
                arrow.rotation = Quaternion.Slerp(arrow.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
            else if (arrow == secondArrow)
            {
                arrow.rotation = Quaternion.Slerp(arrow.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }
        else
        {
            if (arrow.gameObject.activeSelf)
                arrow.gameObject.SetActive(false);
        }
    }
    public void RegisterPrefabSpawn(GameObject scrap)
    {
        if (!spawnedScraps.Contains(scrap))
        {
            spawnedScraps.Add(scrap);
        }
    }
    public void UnregisterPrefab(GameObject scrap)
    {
        if (spawnedScraps.Contains(scrap))
        {
            spawnedScraps.Remove(scrap);
        }
    }
}
