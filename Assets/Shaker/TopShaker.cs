using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;



public class TopShaker : MonoBehaviour
{
    public Transform attachPoint;

    private void Update()
    {
        Vector3 pivotPosition = transform.position;

        Ray ray = new Ray(pivotPosition, Vector3.down);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1f))
        {
            if (hit.collider.CompareTag("Cup"))
            {
                
                // Debug.Log("Collision with shaker");

                GameObject objectToAttach = gameObject;
            
                XRGrabInteractable grabInteractable = objectToAttach.GetComponent<XRGrabInteractable>();
                if (grabInteractable != null)
                {
                    Destroy(grabInteractable);
                }

                Rigidbody rb = objectToAttach.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Destroy(rb);
                }

                BoxCollider boxCollider = objectToAttach.GetComponent<BoxCollider>();
                if (boxCollider != null)
                {
                    // boxCollider.isTrigger = true;
                    Destroy(boxCollider);

                }

                objectToAttach.transform.parent = attachPoint;
                objectToAttach.transform.SetPositionAndRotation(attachPoint.position, attachPoint.rotation);
                objectToAttach.transform.localScale = attachPoint.localScale;


                Destroy(GetComponent<TopShaker>());
                            
            }
            else
            {
                // Debug.DrawRay(pivotPosition, Vector3.down * 1f, Color.red, 3f);

            }


        }
    }


}

