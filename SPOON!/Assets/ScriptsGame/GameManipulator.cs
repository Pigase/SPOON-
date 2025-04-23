using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManipulator : MonoBehaviour
{
    [SerializeField] private GameObject _canvasGameOver;

    private AudioSource[] audio;

    private void Start()
    {
        audio = GetComponents<AudioSource>();
    }

    private void OnEnable()
    {
        Soup.OnSoupRuined += GameOverPanel;
    }

    private void GameOverPanel()
    {
        Time.timeScale = 0f;
        _canvasGameOver.SetActive(true);

        audio[1].Stop();   
    }
}
