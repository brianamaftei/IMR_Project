using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : MonoBehaviour
{

    public GameObject prefabWater;

    private GameObject particleInstance = null;
 
    private void Update()
    {
        Vector3 pivotPosition = transform.position;

        Ray ray = new Ray(pivotPosition, Vector3.down);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 0.4f))
        {
            if (hit.collider.CompareTag("Cup") && particleInstance == null)
            {
                particleInstance = Instantiate(prefabWater, transform.position, Quaternion.identity);
                Debug.Log("Collision with glass");
                Cup cupComponent = hit.collider.gameObject.GetComponent<Cup>();
                Vector3 boxColiderSize = cupComponent.originalColliderSize;
                cupComponent.currentFillLevel = 0.0f;

                if (cupComponent != null)
                {
                    Dictionary<string, ObjectInfo> objectsCup = cupComponent.objectsCup;

                    foreach (var objectEntry in objectsCup)
                    {
                        if (!objectEntry.Value.firstObject.CompareTag("Cup"))
                        {
                            Destroy(objectEntry.Value.firstObject);
                        }

                    }

                    cupComponent.objectsCup.Clear();
                    cupComponent.objectsCup = new Dictionary<string, ObjectInfo>();
                    cupComponent.liquidRenderer.material.SetFloat("_Fill", 0.0f);
                    cupComponent.AddObjectToCup("Liquids", new ObjectInfo("Liquids", 1, hit.collider.gameObject, 0.0f, Color.white));
                    cupComponent.originalColliderSize = boxColiderSize;
                }
            }

            Debug.DrawRay(pivotPosition, Vector3.down * 0.4f, Color.red, 3f);

        }
        else
        {
            if (particleInstance != null)
            {
                Destroy(particleInstance);
            } 
        }
        
    }
}
