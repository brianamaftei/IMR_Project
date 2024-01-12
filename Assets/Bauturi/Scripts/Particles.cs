using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    public float raycastDistance = 5f;
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.forward, out hit, raycastDistance))
        {
            if (hit.collider.CompareTag("Cup"))
            {
                Cup cup = hit.collider.GetComponent<Cup>();
                if (cup != null)
                {
                    cup.HandleCollision(gameObject);
                }
            }

        }
    }
}
