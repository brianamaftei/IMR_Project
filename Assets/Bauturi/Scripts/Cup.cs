using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class Cup : MonoBehaviour
{
    private Renderer liquidRenderer;  
    private float currentFillLevel = 0.0f;
    private BoxCollider boxCollider;

    private Vector3 originalColliderSize; 
    public Color liquidColor;
    private Color initialColor;
    public Transform attachCeleryPoint;
    public Transform attachCherryPoint;
    public Transform attachLemonPoint;
    public Transform attachIcePoint;
    public Transform attachMintPoint;
    public Transform attachOrangePoint;

    public Dictionary<string, ObjectInfo> objectsCup = new Dictionary<string, ObjectInfo>();

    public void Start()
    {
        GameObject cupObject = gameObject;
        liquidRenderer = GetComponent<Renderer>();
        boxCollider = GetComponent<BoxCollider>();
        originalColliderSize = boxCollider.size;
        initialColor = liquidRenderer.material.GetColor("_SideColor");
        AddObjectToCup("Liquids", new ObjectInfo("Liquids", 1, cupObject, liquidRenderer.material.GetFloat("_Fill"), initialColor));
    
    }

    public void HandleCollision(GameObject detectedObject)
    {
        GameObject objectInCup = detectedObject;
        string objectName = objectInCup.tag;
        Debug.Log($"Collision detected with {objectName}!!!!!!");

        Dictionary<string, Transform> attachPoints = new Dictionary<string, Transform>
        {
            { "Celery", attachCeleryPoint },
            { "OrangeSlice", attachOrangePoint },
            { "LemonSlice", attachLemonPoint },
            { "Mint", attachMintPoint },
            { "Ice", attachIcePoint },
            { "Cherry", attachCherryPoint },
            { "Sugar", null}
        };

        if (attachPoints.ContainsKey(objectName))
        {
            if (objectsCup.ContainsKey(objectName))
            {
                objectsCup[objectName].numberOfObjects++;
                Debug.Log("It was added another " + objectName + " into the cup");
            }
            else
            {
                if (objectName != "Sugar")
                   { 
                       AttachObjectToCup(attachPoints[objectName], objectInCup);
                   }
                objectsCup.Add(objectName, new ObjectInfo(objectName, 1, objectInCup));
                Debug.Log("It was added " + objectName + " into the glass");
            }
        }
    }

    private void AddObjectToCup(string objectName, ObjectInfo objectInfo)
    {
        Debug.Log("add object");
        objectsCup[objectName] = objectInfo;
    }

    public void StartFilling()
    {
        if (liquidRenderer != null)
        {
            currentFillLevel = liquidRenderer.material.GetFloat("_Fill");
            currentFillLevel += 0.0005f;
            currentFillLevel = Mathf.Clamp01(currentFillLevel);
            liquidRenderer.material.SetFloat("_Fill", currentFillLevel);
            objectsCup["Liquids"].AddColorToHistory(liquidColor, 0.0005f);
            HandleColors();
            AdjustTopSide(currentFillLevel);
        }
    }


    private void HandleColors()
    {

        Color mixedColor = MixColors();


        Debug.Log("Mixed Color: " + mixedColor);

        liquidRenderer.material.SetColor("_SideColor", mixedColor);
        liquidRenderer.material.SetColor("_TopColor", mixedColor);

    }

    private Color MixColors()
    {
            float totalWeight = 0f;
            float mixedRed = 0f;
            float mixedGreen = 0f;
            float mixedBlue = 0f;
            Dictionary<Color, float> colorHistory = objectsCup["Liquids"].GetColorHistory();
            

            foreach (var entry in colorHistory)
            {
                Color color = entry.Key;
                float fillLevel = entry.Value;

                mixedRed += color.r * fillLevel;
                mixedGreen += color.g * fillLevel;
                mixedBlue += color.b * fillLevel;

                totalWeight += fillLevel;
            }

            mixedRed /= totalWeight;
            mixedGreen /= totalWeight;
            mixedBlue /= totalWeight;

            return new Color(mixedRed, mixedGreen, mixedBlue);
    }


    private void AdjustTopSide(float fillPercentage)
    {
        Vector3 newSize = boxCollider.size;
        newSize.z = originalColliderSize.z * currentFillLevel * 8.0f;

        float maxZSize = originalColliderSize.z;

        newSize.z = Mathf.Min(newSize.z, maxZSize);

        boxCollider.size = newSize;

        Vector3 newCenter = boxCollider.center;
        newCenter.z = newSize.z/2.0f;
        boxCollider.center = newCenter;
    }

    private void AttachObjectToCup(Transform attachPoint, GameObject objectToAttach)
    {
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
        objectToAttach.transform.position = attachPoint.position;
        objectToAttach.transform.rotation = attachPoint.rotation;
    }


    public Dictionary<string, ObjectInfo> GetObjectsInCup()
    {
        return objectsCup;
    }

    public void PrintObjectsInCup()
    {
        foreach (var entry in objectsCup)
        {
            entry.Value.PrintObjectInfo();
        }
    }

}