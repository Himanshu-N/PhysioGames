using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnlargeLoons : MonoBehaviour
{
    private Vector3 ogScale;
    private ControlNScore _controlNScore;
    [SerializeField] private float upFactor = 0.6f;
    bool release = false;

    // Event for when the balloon is released
    public event Action OnBalloonReleased;

    void Start()
    {
        _controlNScore = FindObjectOfType<ControlNScore>();
        _controlNScore.OnNetGripChanged += HandleNetGripChanged;
        ogScale = transform.localScale;
    }

    private void HandleNetGripChanged(float netGrip)
    {
        gameObject.transform.localScale = ogScale * (1 + netGrip);
        if(netGrip > 0.8)
        {
            _controlNScore.OnNetGripChanged -= HandleNetGripChanged;
            release = true;
            OnBalloonReleased?.Invoke();
        }
    }

    private void Update()
    {
        if (release)
        {
            transform.position = gameObject.transform.position + (Vector3.up * Time.deltaTime * upFactor);
            if(transform.position.y > 20)
            {
                Destroy(gameObject);
            }
        }

    }

    private void OnDestroy()
    {
        // Unsubscribe from the event to prevent memory leaks
        _controlNScore.OnNetGripChanged -= HandleNetGripChanged;

    }
}
