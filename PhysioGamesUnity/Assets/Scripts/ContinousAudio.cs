using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinousAudio : MonoBehaviour
{
    private static ContinousAudio audioInstance;

    private void Awake()
    {
        if (audioInstance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            if (SceneManager.GetActiveScene().buildIndex != 2) // Prevents audio in scene index 3
            {
                DontDestroyOnLoad(gameObject);
                audioInstance = this;
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
