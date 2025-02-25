using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject balloonPrefab; // Prefab of the balloon to spawn
    [SerializeField] private Transform spawnPoint; // Point where the balloon will spawn
    [SerializeField] private TextMeshProUGUI _text;
    int score=00;

    private void Start()
    {
        // Find all balloons in the scene at the start and subscribe to their events
        SubscribeToExistingBalloons();
    }

    private void SubscribeToExistingBalloons()
    {
        var balloons = FindObjectsOfType<EnlargeLoons>();
        foreach (var balloon in balloons)
        {
            SubscribeToBalloon(balloon);
        }
    }

    private void SubscribeToBalloon(EnlargeLoons balloon)
    {
        if (balloon != null)
        {
            balloon.OnBalloonReleased += HandleBalloonReleased;
        }
    }

    private void HandleBalloonReleased()
    {
        Debug.Log("A balloon was released!");
        score++;
        _text.text = "SCORE:"+score;
        StartCoroutine(SpawnBalloonAfterDelay());
    }

    private IEnumerator SpawnBalloonAfterDelay()
    {
        yield return new WaitForSeconds(4f);
        SpawnNewBalloon();
    }

    private void SpawnNewBalloon()
    {
        if (balloonPrefab != null && spawnPoint != null)
        {
            GameObject newBalloon = Instantiate(balloonPrefab, spawnPoint.position, Quaternion.identity);

            // Subscribe to the newly spawned balloon's event
            EnlargeLoons enlargeLoons = newBalloon.GetComponent<EnlargeLoons>();
            if (enlargeLoons != null)
            {
                SubscribeToBalloon(enlargeLoons);
            }
            else
            {
                Debug.LogError("The spawned balloon is missing the EnlargeLoons component!");
            }
        }
        else
        {
            Debug.LogError("Balloon prefab or spawn point is not assigned!");
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from events to prevent memory leaks
        var balloons = FindObjectsOfType<EnlargeLoons>();
        foreach (var balloon in balloons)
        {
            balloon.OnBalloonReleased -= HandleBalloonReleased;
        }
    }
}
