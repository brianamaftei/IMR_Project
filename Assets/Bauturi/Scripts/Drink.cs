using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink : MonoBehaviour
{
    public GameObject DrinkObject { get; private set; }
    public Dictionary<string, ObjectInfo> ObjectsInCup { get; set; }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Coaster"))
        {
            Debug.Log("Collision with Coaster Drink");

            
            Transform childTransform = transform.Find("Cup");
            if (childTransform != null)
            {
                GameObject childObject = childTransform.gameObject;
                Cup cupComponent = childObject.GetComponent<Cup>();

                if (cupComponent != null)
                {
                    ObjectsInCup = cupComponent.GetObjectsInCup();
                }
                
                CoasterDrink coasterDrink = collision.gameObject.GetComponent<CoasterDrink>();
                if (coasterDrink != null)
                {
                    coasterDrink.ReceiveCollisionInfo(ObjectsInCup, gameObject);
                }
            }
        }
    }

    public Dictionary<string, ObjectInfo> GetObjectsInCup()
    {
        return ObjectsInCup;
    }

    public GameObject GetDrinkObject()
    {
        return DrinkObject;
    }


}
