using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CoasterDrink : MonoBehaviour
{
    private Dictionary<string, ObjectInfo> ObjectsInCup;
    private GameObject DrinkObject;
    public GameObject[] prefabArray;
    private List<string> TargetTags = new List<string> { "Poco", "Zombie", "Old", "Margarita", "Martini", "Champagne" };

    public ScorePoints sp;

    public void ReceiveCollisionInfo(Dictionary<string, ObjectInfo> objects, GameObject collidedObj)
    {
        if (TargetTags.Contains(collidedObj.tag))
        {
            Debug.Log("Collision with a drink");

            ObjectsInCup = objects;
            DrinkObject = collidedObj;

            Drink drinkComponent = DrinkObject.GetComponent<Drink>();
            if (drinkComponent != null)
            {
                ObjectsInCup = drinkComponent.GetObjectsInCup();
                PrintObjectsInCup();
            }                  

            XRGrabInteractable grabInteractable = DrinkObject.GetComponent<XRGrabInteractable>();
            if (grabInteractable != null)
            {
                if (PlayerPrefs.HasKey("GameState"))
                {
                    string oldState = PlayerPrefs.GetString("GameState");
                    if (oldState == "Day")
                        sp.UpdateScore(ObjectsInCup);
                    else
                        sp.UpdatePoints(ObjectsInCup);
                }
                CreateEmptyGlass(collidedObj.tag, DrinkObject.transform.position);
                Destroy(DrinkObject); 
            }

        }
    }

    private void CreateEmptyGlass(string glassTag, Vector3 spawnPosition)
    {
        List<GameObject> prefabsList =  new List<GameObject>(prefabArray);
        GameObject selectedPrefab = prefabsList.Find(prefab => prefab.tag == glassTag);

        if (selectedPrefab != null)
        {
            spawnPosition.x -= 0.5f;
            Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Prefab with tag " + glassTag + " not found in the list");
        } 
    }

    public GameObject GetDrinkObject()
    {
        return DrinkObject;
    }

    public void PrintObjectsInCup()
    {
        foreach (var entry in ObjectsInCup)
        {
            entry.Value.PrintObjectInfo();
        }
    }

    public string getGlassType()
    {
        return DrinkObject.tag;
    }

    
    public Dictionary<string, ObjectInfo> GetObjectsInCup()
    {
        return ObjectsInCup;
    }
}