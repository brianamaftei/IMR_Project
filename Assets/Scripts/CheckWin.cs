using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWin : MonoBehaviour
{
    public GameObject winScreen;
    void Start()
    {
        if (PlayerPrefs.HasKey("GameWon"))
        {
            string mode = PlayerPrefs.GetString("GameWon");
            if (mode == "True")
            {
                winScreen.SetActive(true);
            }
        }
    }
}
