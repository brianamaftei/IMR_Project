using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CoasterDrink : MonoBehaviour
{
    private Dictionary<string, ObjectInfo> ObjectsInCup;
    private GameObject DrinkObject;
    private List<string> TargetTags = new List<string> { "Poco", "Zombie" };

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
                Destroy(grabInteractable);
            }

            //aici se va trimite dictionarul la tabla de scor

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
