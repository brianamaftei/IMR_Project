using UnityEngine;
using System.Collections.Generic;

public enum GameMode
{
    Creative,
    Learning
}

public class HandleGameState: MonoBehaviour
{
    private GameMode currentMode;
    public GameObject[] allRecipes;

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

                PlayerPrefs.SetString("GameState", "Day");
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
