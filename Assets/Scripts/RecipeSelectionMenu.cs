using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

public class RecipeSelectionMenu : MonoBehaviour
{
    public TextMeshProUGUI recipeNameText;
    public GameObject[] availableRecipes;
    private int currentRecipeIndex = 13;

    void Start()
    {
        UpdateRecipeName();
    }

    public void NextRecipe()
    {
        if (PlayerPrefs.HasKey("GameState"))
        {
            string oldState = PlayerPrefs.GetString("GameState");
            if (oldState == "Day")
            {
                do
                {
                    currentRecipeIndex = (currentRecipeIndex + 1) % availableRecipes.Length;
                } while (availableRecipes[currentRecipeIndex].activeSelf == false);

                UpdateRecipeName();
            }
        }
    }

    public void PreviousRecipe()
    {
        if (PlayerPrefs.HasKey("GameState"))
        {
            string oldState = PlayerPrefs.GetString("GameState");
            if (oldState == "Day")
            {
                do
                {
                    currentRecipeIndex = (currentRecipeIndex - 1 + availableRecipes.Length) % availableRecipes.Length;
                } while (availableRecipes[currentRecipeIndex].activeSelf == false);
                UpdateRecipeName();
            }
        }
    }

    void UpdateRecipeName()
    {
        string formattedName = FormatRecipeName(availableRecipes[currentRecipeIndex].name);
        recipeNameText.text = formattedName;
    }
    public string FormatRecipeName(string name)
    {
        string formattedName = Regex.Replace(name, "Page$", "").Trim();
        formattedName = Regex.Replace(formattedName, "(\\B[A-Z])", " $1");
        return formattedName;
    }

    public void JumpToRecipe(string targetRecipeName)
    {
        for (int i = 0; i < availableRecipes.Length; i++)
        {
            if (availableRecipes[i].activeSelf && availableRecipes[i].name == targetRecipeName)
            {
                currentRecipeIndex = i;
                UpdateRecipeName();
                break;
            }
        }
    }

}
