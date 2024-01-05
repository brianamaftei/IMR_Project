using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfo
{
    public string name;
    public int numberOfObjects;
    public GameObject firstObject;
    public Dictionary<Color, float> colorHistory;

    public ObjectInfo(string tag, int initialCount, GameObject initialObject, float initialFillLevel=0.0f, Color color =  default(Color))
    {
        name=tag;
        numberOfObjects = initialCount;
        firstObject = initialObject;
        InitialiseColorHistory(tag, initialFillLevel, color);
    }

    private void InitialiseColorHistory(string name, float fillLevel, Color initialColor)
    {
        if (name == "Liquids")
        {
            colorHistory = new Dictionary<Color, float>();
            AddColorToHistory(initialColor, fillLevel);
        }
    }

    public void AddColorToHistory(Color color, float fillLevel)
    {
        if (colorHistory != null) 
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
        else
        {
            Debug.LogError("Color history dictionary is null");
        }
    }

    public Dictionary<Color, float> GetColorHistory()
    {
        return colorHistory;
    }

    public void PrintObjectInfo()
    {
        Debug.Log($"Object Info for {name}:");
        Debug.Log($"  Number of Objects: {numberOfObjects}");
        Debug.Log($"  First Object: {firstObject}");
        
        if (colorHistory != null)
        {
            Debug.Log("Color History:");
            foreach (var entry in colorHistory)
            {
                Debug.Log($"  Color: {entry.Key}, Fill Level: {entry.Value}");
            }
        }
    }

}