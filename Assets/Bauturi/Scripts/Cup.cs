using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{
    private Renderer liquidRenderer;  
    private float currentFillLevel = 0.0f;
    private BoxCollider boxCollider;

    private Vector3 originalColliderSize; 
    public Color liquidColor;
    private Color initialColor;
    private Dictionary<Color, float> colorHistory = new Dictionary<Color, float>();


    private void Start()
    {
        liquidRenderer = GetComponent<Renderer>();
        boxCollider = GetComponent<BoxCollider>();
        originalColliderSize = boxCollider.size;
        initialColor = liquidRenderer.material.GetColor("_SideColor");
        AddColorToHistory(initialColor, liquidRenderer.material.GetFloat("_Fill"));
    }

    public void StartFilling()
    {
        if (liquidRenderer != null)
        {
            currentFillLevel = liquidRenderer.material.GetFloat("_Fill");
            currentFillLevel += 0.0005f;
            currentFillLevel = Mathf.Clamp01(currentFillLevel);
            liquidRenderer.material.SetFloat("_Fill", currentFillLevel);
            AddColorToHistory(liquidColor, 0.0005f);
            HandleColors();
            AdjustTopSide(currentFillLevel);
        }
    }

    private void AddColorToHistory(Color color, float fillLevel)
    {
        if (colorHistory.ContainsKey(color))
        {
            colorHistory[color] += fillLevel;
        }
        else
        {
            colorHistory.Add(color, fillLevel);
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


}
