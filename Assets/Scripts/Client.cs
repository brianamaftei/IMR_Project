using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Client : MonoBehaviour
{
    public List<GameObject> clientPrefabs;
    public Transform spawnPoint;
    public Transform bubbleSpawnPoint;
    public TextMeshProUGUI bubbleTextMesh;
    public string clientDrinkName;
    private GameObject currentClientObject;
    private GameObject currentBubbleObject;

    public void SpawnClient()
    {
        int randomIndex = Random.Range(0, clientPrefabs.Count);
        GameObject randomClientPrefab = clientPrefabs[randomIndex];
        currentClientObject = Instantiate(randomClientPrefab, spawnPoint.position, Quaternion.identity);

        ShowBubbleText();
    }

    void ShowBubbleText()
    {           
        bubbleTextMesh.text = clientDrinkName;
        currentBubbleObject = Instantiate(bubbleTextMesh.gameObject, bubbleSpawnPoint.position, Quaternion.identity);
        // StartCoroutine(DestroyBubble(currentBubbleObject, 40f));

    }
    
    // IEnumerator DestroyBubble(GameObject bubbleObject, float delay)
    // {
    //     yield return new WaitForSeconds(delay);

    //     if (bubbleObject != null)
    //     {
    //         DestroyImmediate(bubbleObject, true);
    //     }
    // }


    public void DestroyClient()
    {

        if (currentClientObject != null)
        {
            Destroy(currentClientObject);
            Debug.Log("Client left the bar");
        }
    }

    public GameObject GetClientObject()
    {
        return currentClientObject;
    }

    public void SetDrinkName(string drink)
    {
        clientDrinkName = drink;
    }
}
