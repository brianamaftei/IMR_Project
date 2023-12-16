using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CloneOnGrab : MonoBehaviour
{
    public GameObject prefab;
    private bool wasGrabbed = false;
    public void CloneItem()
    {
        if (!wasGrabbed)
        {
            Vector3 position = transform.position;
            Quaternion rotation = transform.rotation;
            position.y += 0.1f;
            GameObject duplicate = Instantiate(prefab, position, rotation);
            wasGrabbed=true;
            Rigidbody rb = duplicate.GetComponent<Rigidbody>();
            if (rb != null )
            {
                rb.useGravity = true;
                rb.angularDrag = 0.05f;
            }
        }
    }
}
