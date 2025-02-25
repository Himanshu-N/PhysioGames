using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinousBG : MonoBehaviour
{
    private static ContinousBG continousBG;

    private void Awake()
    {
        if (continousBG != null)
        {
            Destroy(gameObject);
        }
        else
        {
            if (SceneManager.GetActiveScene().buildIndex != 2) // Prevents audio in scene index 3
            {
                DontDestroyOnLoad(gameObject);
                continousBG = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 2)// If the scene changes to index 3, destroy audio
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
