using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using System.Linq;


public class ScorePoints : MonoBehaviour
{
    private AudioSource soundManger;
    public AudioClip soundFail;
    public AudioClip soundSuccess;
    public HandleGameState gameState;
    public RewardScreen rs;
    public TextMeshProUGUI scoreBoard;
    public TextMeshProUGUI selectedRecipe; 
    private Dictionary<string, Dictionary<string, float>> recipes = new Dictionary<string, Dictionary<string, float>>();
    private float score = 0;
    private float points = 0;
    private float newScore = 0;
    private float newPoints = 0;
    private int dayCounter = 1;

    private Client currentClient;

    private GameObject HandleScorePoints;
    private ScorePoints scorePointsComponent;

    private GameObject Canvas;
    private RecipeSelectionMenu canvasComponent;

    private List<GameObject> newListRecipes = new List<GameObject>();
    private int currentIndex = 0;


    private void Start()
    {
        soundManger = GetComponent<AudioSource>();
        Debug.Log("Start method called in ScorePoints");
        Dictionary<string, float> recipe = new Dictionary<string, float>
        {
            { "RGBA(0.898, 0.220, 0.137, 1.000)", 0.8f },
            { "RGBA(0.851, 0.855, 0.851, 1.000)", 0.2f },
            { "Celery", 1f },
            { "Zombie", 1f }
        };
        recipes.Add("Bloody Mary", recipe);

        recipe = new Dictionary<string, float>
        {
            { "RGBA(0.969, 0.949, 0.655, 1.000)", 0.25f },
            { "RGBA(0.957, 0.847, 0.467, 1.000)", 0.25f },
            { "RGBA(0.706, 0.886, 0.843, 1.000)", 0.5f },
            { "Cherry", 1f },
            { "Poco", 1f }
        };
        recipes.Add("Pina Colada", recipe);

        recipe = new Dictionary<string, float>
        {
            { "RGBA(0.925, 0.925, 0.890, 1.000)", 0.3f },
            { "RGBA(0.741, 0.741, 0.737, 1.000)", 0.7f },
            { "Ice", 1f },
            { "Old", 1f }
        };
        recipes.Add("Gin Tonic", recipe);

        recipe = new Dictionary<string, float>
        {
            { "RGBA(0.702, 0.878, 0.835, 1.000)", 0.33f },
            { "RGBA(0.925, 0.925, 0.890, 1.000)", 0.33f },
            { "RGBA(0.890, 1.000, 0.000, 1.000)", 0.34f },
            { "Margarita", 1f }
        };
        recipes.Add("White Lady", recipe);

        recipe = new Dictionary<string, float>
        {
            { "RGBA(0.741, 0.741, 0.737, 1.000)", 0.8f },
            { "RGBA(0.906, 0.467, 0.243, 1.000)", 0.2f },
            { "Mint", 1f },
            { "Sugar", 1f },
            { "Zombie", 1f }
        };
        recipes.Add("Mint Julep", recipe);

        recipe = new Dictionary<string, float>
        {
            { "RGBA(1.000, 0.670, 0.000, 1.000)", 0.25f },
            { "RGBA(0.937, 0.439, 0.310, 1.000)", 0.25f },
            { "RGBA(0.398, 0.110, 0.039, 1.000)", 0.5f },
            { "OrangeSlice", 1f },
            { "Poco", 1f }
        };
        recipes.Add("Tequila Sunrise", recipe);

        recipe = new Dictionary<string, float>
        {
            { "RGBA(0.851, 0.855, 0.851, 1.000)", 0.2f },
            { "RGBA(0.200, 0.940, 0.000, 1.000)", 0.2f },
            { "RGBA(0.765, 0.000, 0.078, 0.525)", 0.3f },
            { "RGBA(0.702, 0.878, 0.835, 1.000)", 0.2f },
            { "Martini", 1f }
        };
        recipes.Add("Cosmopolitan", recipe);

        recipe = new Dictionary<string, float>
        {
            { "RGBA(0.200, 0.940, 0.000, 0.525)", 0.5f },
            { "RGBA(0.925, 0.925, 0.890, 1.000)", 0.5f },
            { "Martini", 1f }
        };
        recipes.Add("Gimlet", recipe);

        recipe = new Dictionary<string, float>
        {
            { "RGBA(0.494, 0.270, 0.341, 1.000)", 0.3f },
            { "RGBA(0.710, 0.149, 0.118, 1.000)", 0.5f },
            { "RGBA(0.925, 0.925, 0.890, 1.000)", 0.2f },
            { "Old", 1f }
        };
        recipes.Add("Negroni", recipe);

        recipe = new Dictionary<string, float>
        {
            { "RGBA(0.398, 0.110, 0.039, 1.000)", 0.6f },
            { "RGBA(0.890, 1.000, 0.000, 1.000)", 0.4f },
            { "Margarita", 1f }
        };
        recipes.Add("Margarita", recipe);

        recipe = new Dictionary<string, float>
        {
            { "RGBA(0.962, 0.140, 0.221, 1.000)", 0.3f },
            { "RGBA(0.765, 0.000, 0.078, 1.000)", 0.5f },
            { "RGBA(0.773, 0.868, 0.841, 1.000)", 0.2f },
            { "Old", 1f }
        };
        recipes.Add("Hot Rod", recipe);

        recipe = new Dictionary<string, float>
        {
            { "RGBA(0.008, 0.866, 0.000, 1.000)", 0.6f },
            { "RGBA(0.992, 0.859, 0.518, 1.000)", 0.4f },
            { "Champagne", 1f }
        };
        recipes.Add("The Ghost", recipe);

        recipe = new Dictionary<string, float>
        {
            { "RGBA(1.000, 0.670, 0.000, 1.000)", 0.3f },
            { "RGBA(0.765, 0.000, 0.078, 1.000)", 0.2f },
            { "RGBA(0.973, 0.788, 0.196, 1.000)", 0.2f },
            { "RGBA(0.851, 0.855, 0.851, 1.000)", 0.3f },
            { "OrangeSlice", 1f },
            { "Poco", 1f }
        };
        recipes.Add("Sex On The Beach", recipe);

        recipe = new Dictionary<string, float>
        {
            { "RGBA(0.827, 0.906, 0.490, 1.000)", 0.15f },
            { "RGBA(0.702, 0.878, 0.835, 1.000)", 0.15f },
            { "RGBA(0.925, 0.925, 0.890, 1.000)", 0.15f },
            { "RGBA(0.851, 0.855, 0.851, 1.000)", 0.15f },
            { "RGBA(0.953, 0.953, 0.953, 1.000)", 0.15f },
            { "RGBA(0.398, 0.110, 0.039, 1.000)", 0.15f },
            { "RGBA(0.227, 0.122, 0.110, 1.000)", 0.1f },
            { "LemonSlice", 1f },
            { "Ice", 1f },
            { "Zombie", 1f }
        };
        recipes.Add("Long Island Ice Tea", recipe);
    }

    public void UpdateScore(Dictionary<string, ObjectInfo> objects, string glassTag)
    {
        newScore += UpdateValue(objects, glassTag);
        scoreBoard.text = $"Score: {score + newScore}";
        if (PlayerPrefs.HasKey("GameState"))
        {
            string oldState = PlayerPrefs.GetString("GameState");
            if (oldState == "Day" && newScore >= -1000)//1000 + dayCounter * 150)
            {
                gameState.SwitchGameState();
                score += newScore;
                newScore = 0;
                StartingTheNight();
            }
        }
    }
    
    List<GameObject> GenerateRecipeList(int numberOfRecipes)
    {
        List<GameObject> generatedList = new List<GameObject>();

        HandleGameState handleGameState = FindObjectOfType<HandleGameState>();
        HandleGameState gameComponent = handleGameState.GetComponent<HandleGameState>();
        GameObject[] allRecipes = gameComponent.allRecipes;

        GameObject[] activeRecipes = allRecipes.Where(recipe => recipe.activeSelf).ToArray();

        if (activeRecipes.Length > 0)
        {
            for (int i = 0; i < numberOfRecipes; i++)
            {
                int randomIndex = UnityEngine.Random.Range(0, activeRecipes.Length);
                GameObject randomRecipe = activeRecipes[randomIndex];
                generatedList.Add(randomRecipe);
            }
        }
        return generatedList;
    }

    void StartingTheNight()
    {
        newListRecipes = GenerateRecipeList(5);
        PlayerPrefs.SetString("ClientsLeft", "True");
        NextRecipe();        
    }

    public void NextRecipe()
    {
        if (currentIndex < newListRecipes.Count)
        {
            GameObject currentRecipe = newListRecipes[currentIndex];
            currentIndex++;
            Debug.Log(currentIndex);
            Canvas= GameObject.Find("Canvas");
            canvasComponent = Canvas.GetComponent<RecipeSelectionMenu>();
            canvasComponent.JumpToRecipe(currentRecipe.name);
            string formattedName = canvasComponent.FormatRecipeName(currentRecipe.name);

            NewClient(formattedName);
        }
        else
        {
            EndingTheNight();
        }
    }

    public void NewClient(string currentRecipeName)
    {
        Client clientComponent = FindObjectOfType<Client>();
        if (clientComponent != null)
        {
            clientComponent.SetDrinkName(currentRecipeName);
            clientComponent.SpawnClient();

        }
        
        SetCurrentClient(clientComponent);
    }

    void EndingTheNight()
    {
        currentIndex = 0;
        newListRecipes.Clear();
        PlayerPrefs.SetString("ClientsLeft", "False");
    }


    public void UpdatePoints(Dictionary<string, ObjectInfo> objects, string glassTag)
    {
        newPoints += UpdateValue(objects, glassTag);
        scoreBoard.text = $"Points: {points + newPoints}";
        currentClient.DestroyClient();
        NextRecipe();
        if (PlayerPrefs.HasKey("ClientsLeft"))
        {
            string oldState = PlayerPrefs.GetString("ClientsLeft");
            print(oldState);
            if (oldState == "False")
            {
                if (newPoints >= 1000) // + dayCounter * 150)
                {
                    soundManger.clip = soundSuccess;
                    soundManger.Play();
                    rs.ActivateCanvas();
                }
                else
                {
                    soundManger.clip = soundFail;
                    soundManger.Play();
                    gameState.SwitchGameState();
                }
                dayCounter += 1;
                points += newPoints;
                newPoints = 0;
            }
        }
    }
    private float UpdateValue(Dictionary<string, ObjectInfo> objects, string glassTag)
    {
        float val = 0f;
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
        if (recipe.ContainsKey(glassTag))
        {
            val += 50f;
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
