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
    private AudioSource soundManager;
    public ScorePoints sp;

    public bool doCollision = true;

    public bool shakerIsUsed = false;
    public GameObject TopShakerPrefab;
    public GameObject ShakerPrefab;
    public Transform TopShakerPoint;
    public Transform ShakerPoint;

    public bool shakerScore = false;

    private void Start()
    {
        soundManager = GetComponent<AudioSource>();
    }

    public void ReceiveCollisionInfo(Dictionary<string, ObjectInfo> objects, GameObject collidedObj)
    {
        if (TargetTags.Contains(collidedObj.tag) && doCollision)
        {
            doCollision = false;
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
                        sp.UpdateScore(ObjectsInCup, collidedObj.tag, shakerScore);
                    else
                        sp.UpdatePoints(ObjectsInCup, collidedObj.tag, shakerScore);
                }
                else
                {
                    sp.UpdateScore(ObjectsInCup, collidedObj.tag, shakerScore);
                }
                CreateEmptyGlass(collidedObj.tag, transform.position);
                Destroy(DrinkObject);

                DestroyAndCreateShaker();

                soundManager.Play();
            }
            doCollision = true;

        }
    }

    private void CreateEmptyGlass(string glassTag, Vector3 spawnPosition)
    {
        List<GameObject> prefabsList = new List<GameObject>(prefabArray);
        GameObject selectedPrefab = prefabsList.Find(prefab => prefab.tag == glassTag);

        if (selectedPrefab != null)
        {
            //Vector3 prefabScale = selectedPrefab.transform.localScale;
            spawnPosition.x -= 0.7f; //* prefabScale.x;
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

    public void DestroyAndCreateShaker()
    {
        GameObject topShaker = GameObject.FindGameObjectWithTag("TopShaker");
        GameObject shaker = GameObject.FindGameObjectWithTag("Shaker");


        if (shaker != null)
        {
            Shaker shakerComponent = shaker.GetComponent<Shaker>();

            if (shakerComponent != null)
            {
                if (shakerComponent.movementCount > 20)
                {
                    shakerScore = true;
                    Debug.Log("Shakerul a fost folosit");
                }
            }
        }

        if (topShaker != null)
        {
            Destroy(topShaker);
        }

        if (shaker != null)
        {
            Destroy(shaker);
        }

        GameObject newTopShaker = Instantiate(TopShakerPrefab, TopShakerPoint.position, TopShakerPoint.rotation);

        GameObject newShaker = Instantiate(ShakerPrefab, ShakerPoint.position, ShakerPoint.rotation);

        Transform attachCapacPoint = newShaker.transform.Find("attach_capac");

        if (attachCapacPoint != null)
        {
            TopShaker topShakerComponent = newTopShaker.GetComponent<TopShaker>();
            if (topShakerComponent != null)
            {
                topShakerComponent.attachPoint = attachCapacPoint;
            }

        }


        Transform attachPoint = newTopShaker.transform.Find("attach");

        if (attachPoint != null)
        {
            XRGrabInteractable grabInteractable = newTopShaker.GetComponent<XRGrabInteractable>();
            if (grabInteractable != null)
            {
                grabInteractable.attachTransform = attachPoint;
            }

        }

    }

}