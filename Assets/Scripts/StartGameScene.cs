using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameScene : MonoBehaviour
{
    private AudioSource soundManager;
    private void Start()
    {
        soundManager = GetComponent<AudioSource>();
    }
    public void StartCreative() 
    {
        PlayerPrefs.SetString("GameMode", "Creative");
        soundManager.Stop();
        SceneManager.LoadSceneAsync("GameScene");
    }
    public void StartLearning()
    {
        PlayerPrefs.SetString("GameMode", "Learning");
        soundManager.Stop();
        SceneManager.LoadSceneAsync("GameScene");
    }
}
