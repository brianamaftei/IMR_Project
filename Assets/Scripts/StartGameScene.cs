using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameScene : MonoBehaviour
{
    public void StartCreative() 
    {
        PlayerPrefs.SetString("GameMode", "Creative");
        SceneManager.LoadSceneAsync("GameScene");
    }
    public void StartLearning()
    {
        PlayerPrefs.SetString("GameMode", "Learning");
        SceneManager.LoadSceneAsync("GameScene");
    }
}
