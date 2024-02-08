using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    public Dictionary<string, ObjectInfo> objectsCup;
    private Cup componentCupOfTheGlass;

    public Transform originPoint;

    [SerializeField]
    private Material liquid;

    private bool hitted = false;


    private Vector3 lastPosition;
    public int movementCount = 0;

    void Start()
    {
        lastPosition = transform.position;
    }

    private void Update()
    {
        //movement of the shaker
        if (transform.position != lastPosition)
        {
            movementCount++;

            lastPosition = transform.position;
        }

        liquid.color = gameObject.GetComponentInChildren<Cup>().liquidRenderer.material.GetColor("_SideColor");

        Vector3 pivotPosition = originPoint.position;

        Ray ray = new Ray(pivotPosition, Vector3.down);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2f))
        {
            if (hit.collider.CompareTag("Cup"))
            {
                string objectName = hit.collider.gameObject.name;
                if (objectName != "CupShaker")
                {
                    Debug.Log("Hit another cup");
                    hitted = true;
                    componentCupOfTheGlass = hit.collider.gameObject.GetComponent<Cup>();
                    componentCupOfTheGlass.liquidColor = liquid.color;
                }

            }
            else
            {
                if(hitted == true)
                {
                    UpdateDictionary();
                    hitted = false;
                }
            }
        }



        // Debug.DrawRay(pivotPosition, Vector3.down * 2f, Color.red, 3f);
    }


    public void UpdateDictionary()
    {
        Cup componentCupShaker = gameObject.GetComponentInChildren<Cup>();

        componentCupOfTheGlass.objectsCup = new Dictionary<string, ObjectInfo>();

        foreach (var kvp in componentCupShaker.objectsCup)
        {
            componentCupOfTheGlass.objectsCup[kvp.Key] = kvp.Value;
        }
        
    }
}
