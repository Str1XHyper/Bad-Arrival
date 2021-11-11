using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    SceneManager sceneManager = new SceneManager();

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
