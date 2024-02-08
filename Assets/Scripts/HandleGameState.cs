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
    private AudioSource soundManager;
    private AudioSource talking;
    public AudioClip soundDay, soundNight, talkingSound;

    void Start()
    {
        soundManager = GetComponent<AudioSource>();
        soundManager.clip = soundDay;
        soundManager.Play();
        talking = gameObject.AddComponent<AudioSource>();
        talking.clip = talkingSound;
        talking.loop = true;
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
                PlayerPrefs.DeleteKey("GameState");
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
    
    public void AddRecipe(string recipeName)
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
    public void StopAudio()
    {
        if (PlayerPrefs.HasKey("GameState"))
        {
            string oldState = PlayerPrefs.GetString("GameState");
            if (oldState == "Day")
            {
                soundManager.Stop();
            }
            else
            {
                soundManager.Stop();
                talking.Stop();
            }
        }
    }
    
    public void SwitchGameState()
    {
        if (PlayerPrefs.HasKey("GameState"))
        {
            string oldState = PlayerPrefs.GetString("GameState");
            if (oldState == "Day")
            {
                PlayerPrefs.SetString("GameState", "Night");
                soundManager.Stop();
                soundManager.clip = soundNight;
                soundManager.volume = 0.1f;
                soundManager.Play();
                talking.Play();
            }

            else
            {
                PlayerPrefs.SetString("GameState", "Day");
                soundManager.Stop();
                talking.Stop();
                soundManager.clip = soundDay;
                soundManager.volume = 0.25f;
                soundManager.Play();
            }
                
        }
    }

}
