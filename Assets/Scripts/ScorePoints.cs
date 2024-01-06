using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class ScorePoints : MonoBehaviour
{
    public GameObject rs;
    public TextMeshProUGUI scoreBoard;
    public TextMeshProUGUI selectedRecipe; 
    private Dictionary<string, Dictionary<string, float>> recipes = new Dictionary<string, Dictionary<string, float>>();
    private float score = 0;
    private float points = 0;
    private float newScore = 0;
    private float newPoints = 0;
    private int dayCounter = 1;

    private Client currentClient;

    private void Start()
    {   
        Debug.Log("Start method called in ScorePoints");
        Dictionary<string, float> recipe = new Dictionary<string, float>();
        recipe.Add("RGBA(0.898, 0.220, 0.137, 1.000)", 0.8f);
        recipe.Add("RGBA(0.851, 0.855, 0.851, 1.000)", 0.2f);
        recipe.Add("Celery", 1f);
        recipes.Add("Bloody Mary", recipe);

        recipe = new Dictionary<string, float>();
        recipe.Add("RGBA(0.969, 0.949, 0.655, 1.000)", 0.25f);
        recipe.Add("RGBA(0.957, 0.847, 0.467, 1.000)", 0.25f);
        recipe.Add("RGBA(0.706, 0.886, 0.843, 1.000)", 0.5f);
        recipe.Add("Cherry", 1f);
        recipes.Add("Pina Colada", recipe);
    }
    public void UpdateScore(Dictionary<string, ObjectInfo> objects)
    {
        newScore += UpdateValue(score, objects);
        scoreBoard.text = $"Score: {score + newScore}";
        if (newScore >= 1500 + dayCounter * 100)
            PlayerPrefs.SetString("GameState", "Night");
    }

    public void UpdatePoints(Dictionary<string, ObjectInfo> objects)
    {
        newPoints = UpdateValue(points, objects);
        scoreBoard.text = $"Points: {points + newPoints}";
        if (PlayerPrefs.HasKey("ClientsLeft"))
        {
            string oldState = PlayerPrefs.GetString("ClientsLeft");
            if (oldState == "False")
            {
                if (newPoints >= 1500 + dayCounter * 100)
                {
                    rs.GetComponent<RewardScreen>().ActivateCanvas();
                }
                else
                {
                    PlayerPrefs.SetString("GameState", "Day");
                }
                dayCounter += 1;
                points += newPoints;
            }
        }

        currentClient.DestroyClient(); 
        HandleGameState handleGameState = FindObjectOfType<HandleGameState>();
        HandleGameState gameComponent = handleGameState.GetComponent<HandleGameState>();
        gameComponent.NextRecipe();       

    }
    private float UpdateValue(float val, Dictionary<string, ObjectInfo> objects)
    {
        var recipe = recipes[selectedRecipe.text];
        var includedKeys = new HashSet<string>();
        foreach (var entry in objects)
        {
            if (entry.Value.name == "Liquids")
            {
                float sum = 0f;
                foreach (var item in entry.Value.colorHistory)
                {
                    sum += item.Value;
                }
                foreach (var item in entry.Value.colorHistory)
                {
                    if (recipe.ContainsKey(item.Key.ToString()))
                    {
                        float recipeValue = recipe[item.Key.ToString()];
                        float itemValue = item.Value / sum;
                        float difference = Math.Abs(recipeValue - itemValue);
                        val += 100f - difference * 100;
                        includedKeys.Add(item.Key.ToString());
                    }
                    else if (item.Value > 0.0001f)
                    {
                        val -= 100f;
                    }
                }
            }
            else
            {
                if (recipe.ContainsKey(entry.Value.name))
                {
                    val += 100f;
                    includedKeys.Add(entry.Value.name);
                }
            }
        }
        foreach (var recipeKey in recipe.Keys)
        {
            if (!includedKeys.Contains(recipeKey))
            {
                val -= 50f;
            }
        }
        return val;
    }

    public void SetCurrentClient(Client newClient)
    {
        currentClient = newClient;
        if (currentClient == null)
        {
            Debug.LogWarning("No client");

        }
        else
        {
            Debug.Log(currentClient.clientDrinkName);
        }
    }

}
