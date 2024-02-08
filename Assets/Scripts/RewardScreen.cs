using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RewardScreen : MonoBehaviour
{
    public HandleGameState gameState;
    public GameObject canvas;
    public GameObject[] buttons;
    public GameObject[] allRecipes;
    private int numRecipes;
    private GameObject[] inactiveRecipePages = new GameObject[3];
    private GameObject[] selectedRecipes = new GameObject[3];
    private AudioSource soundManager;

    void Start()
    {
        soundManager = GetComponent<AudioSource>();
    }

    public void ActivateCanvas()
    {
        canvas.SetActive(true);
        GetRandomInactiveRecipePages();
        AssignRandomRecipePagesToButtons();
    }

    void AssignRandomRecipePagesToButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (numRecipes > i)
            {
                RectTransform buttonRect = buttons[i].GetComponent<RectTransform>();

                Vector3 buttonWorldPos = buttonRect.TransformPoint(buttonRect.rect.center);
                Vector3 newPosition = new Vector3(buttonWorldPos.x, buttonWorldPos.y, buttonWorldPos.z);
                Quaternion newRotation = Quaternion.Euler(-90f, -180f, 0f);
                GameObject recipeCopy = Instantiate(selectedRecipes[i], newPosition, newRotation);
                recipeCopy.SetActive(true);
                Rigidbody rb = recipeCopy.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.useGravity = false;
                }

                inactiveRecipePages[i] = recipeCopy;
            }
        }
    }

    void GetRandomInactiveRecipePages()
    {
        GameObject[] inactiveRecipes = Array.FindAll(allRecipes, recipe => !recipe.activeSelf);
        numRecipes = Mathf.Min(inactiveRecipes.Length, 3); // Select at most 3 inactive recipes
        if (numRecipes == 0)
        {
            gameState.StopAudio();
            PlayerPrefs.SetString("GameWon", "True");
            SceneManager.LoadSceneAsync("MainMenu");
        }
        for (int i = 0; i < numRecipes; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, inactiveRecipes.Length);
            selectedRecipes[i] = inactiveRecipes[randomIndex];
            inactiveRecipes[randomIndex] = inactiveRecipes[inactiveRecipes.Length - 1]; // Swap with the last inactive recipe
            Array.Resize(ref inactiveRecipes, inactiveRecipes.Length - 1); // Exclude the selected recipe
        }
    }

    public void OnLeftButtonClick()
    {
        if (numRecipes > 0)
        {
            foreach (GameObject page in inactiveRecipePages)
                Destroy(page);
            selectedRecipes[0].SetActive(true); // Activate the original recipe page
            soundManager.Play();
            gameState.SwitchGameState();
            canvas.SetActive(false); // Deactivate the canvas
        }
    }
    public void OnCenterButtonClick()
    {
        if (numRecipes > 1)
        {
            foreach (GameObject page in inactiveRecipePages)
                Destroy(page);
            selectedRecipes[1].SetActive(true); // Activate the original recipe page
            soundManager.Play();
            gameState.SwitchGameState();
            canvas.SetActive(false); // Deactivate the canvas
        }
    }
    public void OnRightButtonClick()
    {
        if (numRecipes > 2)
        {
            foreach (GameObject page in inactiveRecipePages)
                Destroy(page);
            selectedRecipes[2].SetActive(true); // Activate the original recipe page
            soundManager.Play();
            gameState.SwitchGameState();
            canvas.SetActive(false); // Deactivate the canvas
        }
    }
}
