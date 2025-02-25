using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManagerScript : MonoBehaviour
{
    public void OnStartClicked()
    {
        SceneManager.LoadScene(1);
    }
    public void OnBackClicked()
    {
        SceneManager.LoadScene(0);
    }
    public void OnBalloonyClicked() { 
        SceneManager.LoadScene(2); 
    }

    public void OnExitClicked()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
