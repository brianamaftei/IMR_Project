using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientRayCast : MonoBehaviour
{
    void Update()
    {
        float radius = 0.02f;
        RaycastHit hit;
        Bounds bounds = GetComponent<BoxCollider>().bounds;
        LayerMask collisionLayer = LayerMask.GetMask("Default");

        Vector3 center = bounds.center;

        if (Physics.SphereCast(center, radius, Vector3.down, out hit, Mathf.Infinity, collisionLayer))
        {

             if (hit.collider.CompareTag("Cup") && hit.distance <= 0.05f)
            {
                hit.collider.GetComponent<Cup>().HandleCollision(gameObject);
                Destroy(GetComponent<IngredientRayCast>());
            } 
        }
    }
}
