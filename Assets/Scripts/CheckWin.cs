using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWin : MonoBehaviour
{
    private AudioSource soundManager;
    public GameObject winScreen;
    void Start()
    {
        soundManager = GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("GameWon"))
        {
            string mode = PlayerPrefs.GetString("GameWon");
            if (mode == "True")
            {
                soundManager.Play();
                winScreen.SetActive(true);
                PlayerPrefs.SetString("GameWon", "False");
            }
        }
    }
}
