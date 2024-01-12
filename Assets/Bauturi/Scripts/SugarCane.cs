using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugarCane : MonoBehaviour
{
    public GameObject particlesPrefab;
    public Transform spawnPoint;
    private GameObject spawnedPrefab;

    void Update()
    {
        if (Mathf.Asin(transform.forward.y) * Mathf.Rad2Deg >= 45)
        {
            if (spawnedPrefab == null)
            {
                spawnedPrefab = Instantiate(particlesPrefab, spawnPoint.position, spawnPoint.rotation);
                spawnedPrefab.transform.parent = spawnPoint;

            }
        }
        else
        {
            if (spawnedPrefab != null)
            {
                Destroy(spawnedPrefab);
                spawnedPrefab = null;
            }
        }
    }
}
