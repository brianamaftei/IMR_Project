using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartCreativeScene : MonoBehaviour
{
    public void StartCreative() 
    {
        SceneManager.LoadSceneAsync("CreativeScene");
    }
}
