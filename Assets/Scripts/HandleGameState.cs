using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public enum GameMode
{
    Creative,
    Learning
}

public class HandleGameState: MonoBehaviour
{
    private GameMode currentMode;
    public GameObject[] allRecipes;

    private GameObject HandleScorePoints;
    private ScorePoints scorePointsComponent;

    private GameObject Canvas;
    private RecipeSelectionMenu canvasComponent;

    private List<GameObject> newListRecipes = new List<GameObject>();
    private int currentIndex = 0;

    void Start()
    {
        if (PlayerPrefs.HasKey("GameMode"))
        {
            string mode = PlayerPrefs.GetString("GameMode");
            if (mode == "Creative")
            {
                currentMode = GameMode.Creative;
                foreach (GameObject recipe in allRecipes)
                {
                    recipe.SetActive(true);
                }
            }
            else if (mode == "Learning")
            {
                currentMode = GameMode.Learning;
                AddRecipe("BloodyMaryPage");
                AddRecipe("PinaColadaPage");

                PlayerPrefs.SetString("GameState", "Night");
                StartingTheNight();
            }
        }
    }
    
    void AddRecipe(string recipeName)
    {
        foreach (GameObject recipe in allRecipes)
        {
            if (recipe.name == recipeName)
            {
                recipe.SetActive(true);
                return;
            }
        }

    }
    
    List<GameObject> GenerateRecipeList(int numberOfRecipes)
    {
        List<GameObject> generatedList = new List<GameObject>();

        GameObject[] activeRecipes = allRecipes.Where(recipe => recipe.activeSelf).ToArray();

        if (activeRecipes.Length > 0)
        {
            for (int i = 0; i < numberOfRecipes; i++)
            {
                int randomIndex = Random.Range(0, activeRecipes.Length);
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
        HandleScorePoints= GameObject.Find("HandleScorePoints");
        scorePointsComponent = HandleScorePoints.GetComponent<ScorePoints>();
        scorePointsComponent.SetCurrentClient(clientComponent);
    }

    void EndingTheNight()
    {
        currentIndex = 0;
        newListRecipes.Clear();
        PlayerPrefs.SetString("ClientsLeft", "False");
    }

    void SwitchGameState()
    {
        if (PlayerPrefs.HasKey("GameState"))
        {
            string oldState = PlayerPrefs.GetString("GameState");
            if (oldState == "Day")
                PlayerPrefs.SetString("GameState", "Night");
            else
                PlayerPrefs.SetString("GameState", "Day");
        }
    }

}
