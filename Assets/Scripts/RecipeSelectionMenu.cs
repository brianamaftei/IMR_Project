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
        do
        {
            currentRecipeIndex = (currentRecipeIndex + 1) % availableRecipes.Length;
        } while (availableRecipes[currentRecipeIndex].activeSelf == false);
        
        UpdateRecipeName();
    }

    public void PreviousRecipe()
    {
        do
        {
            currentRecipeIndex = (currentRecipeIndex - 1 + availableRecipes.Length) % availableRecipes.Length;
        } while (availableRecipes[currentRecipeIndex].activeSelf == false);
        UpdateRecipeName();
    }

    void UpdateRecipeName()
    {
        string formattedName = FormatRecipeName(availableRecipes[currentRecipeIndex].name);
        recipeNameText.text = formattedName;
    }
    string FormatRecipeName(string name)
    {
        string formattedName = Regex.Replace(name, "Page$", "").Trim();
        formattedName = Regex.Replace(formattedName, "(\\B[A-Z])", " $1");
        return formattedName;
    }
}
