using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Client : MonoBehaviour
{
    public List<GameObject> clientPrefabs;
    public Transform spawnPoint;
    public Transform bubbleSpawnPoint;
    public TextMeshProUGUI bubbleTextMesh;
    public string clientDrinkName;
    private GameObject currentClientObject;
    private GameObject currentBubbleObject;

    public Transform[] pathPoints;
    private Transform[] initialPathPoints;
    public float duration = 5f;

    public void SpawnClient()
    {
        int randomIndex = Random.Range(0, clientPrefabs.Count);
        
        GameObject randomClientPrefab = clientPrefabs[randomIndex];
        currentClientObject = Instantiate(randomClientPrefab, spawnPoint.position, Quaternion.Euler(0, 90, 0));


        initialPathPoints = new Transform[pathPoints.Length];
        pathPoints.CopyTo(initialPathPoints, 0);

        MoveOnPath();
    }

    void MoveOnPath()
    {
        
        GameObject coaster= GameObject.Find("Drink Coaster");
        CoasterDrink coasterDrinkComponent = coaster.GetComponent<CoasterDrink>();
        coasterDrinkComponent.doCollision = false;
        
        Transform transformClient = currentClientObject.transform;

        Vector3[] pathPositions = new Vector3[pathPoints.Length];
        for (int i = 0; i < pathPoints.Length; i++)
        {
            pathPositions[i] = pathPoints[i].position;
        }
               
        Tween t = transformClient.DOPath(pathPositions, duration, PathType.Linear)
            .SetOptions(false);     

        t.OnComplete(() =>
        {
            currentClientObject.transform.Rotate(Vector3.up, -90.0f);

            ShowBubbleText();

            coasterDrinkComponent.doCollision = true;         
        });

    }

    void ShowBubbleText()
    {           
        bubbleTextMesh.text = clientDrinkName;
        currentBubbleObject = Instantiate(bubbleTextMesh.gameObject, bubbleSpawnPoint.position, Quaternion.identity);
        // StartCoroutine(DestroyBubble(currentBubbleObject, 40f));

    }

    void MoveBack()
    {
        System.Array.Reverse(initialPathPoints);

        GameObject copyOfClientObject = currentClientObject;

        Transform transformClient = copyOfClientObject.transform;

        Vector3[] reversedPathPositions = new Vector3[initialPathPoints.Length];
        for (int i = 0; i < initialPathPoints.Length; i++)
        {
            reversedPathPositions[i] = initialPathPoints[i].position;
        }

        copyOfClientObject.transform.Rotate(Vector3.up, -90.0f);

        Tween t = transformClient.DOPath(reversedPathPositions, duration, PathType.Linear);

        t.OnComplete(() =>
        {
            DestroyObject(copyOfClientObject);
        });
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
        MoveBack();        
    }

    public void DestroyObject(GameObject copyOfClientObject)
    {
        if (copyOfClientObject != null)
        {
            Destroy(copyOfClientObject);
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
