using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowController : MonoBehaviour
{
    public Material dayMaterial;
    public Material nightMaterial;
    
    private string oldState;

    void Start()
    {
        oldState = PlayerPrefs.GetString("GameState");
        GetComponent<Renderer>().material = dayMaterial;

    }

    void Update()
    {
        string currentState = PlayerPrefs.GetString("GameState");
        if (currentState != oldState)
        {
            oldState = currentState;
            UpdateMaterial();
        }
    }

    void UpdateMaterial()
    {
        Material materialToUpdate = (oldState == "Day") ? dayMaterial : nightMaterial;

        if (materialToUpdate != null)
        {
            GetComponent<Renderer>().material = materialToUpdate;
        }

    }
}