using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GameManipulator : MonoBehaviour
{
    [SerializeField] private int _numberWave=1;
    [SerializeField] private GameObject _canvasGameOver;

    private AudioSource[] audio;

    public static Action NewWave;
    public static Action<int> NumberWave;

    private void Start()
    {
        audio = GetComponents<AudioSource>();
    }

    private void OnEnable()
    {
        Soup.OnSoupRuined += GameOverPanel;
        Soup.OnSoupFull += NextWave;
    }
    private void OnDisable()
    {
        Soup.OnSoupRuined -= GameOverPanel;
        Soup.OnSoupFull -= NextWave;
    }

    private void GameOverPanel()
    {
        Time.timeScale = 0f;
        _canvasGameOver.SetActive(true);

        audio[1].Stop();   
    }
    private void NextWave()
    {
        if (_numberWave == 1)
        {
            NewWave?.Invoke();
            _numberWave++;
            NumberWave.Invoke(_numberWave);
        }
        else if (_numberWave == 2)
        {
            NewWave?.Invoke();
            _numberWave++;
            NumberWave.Invoke(_numberWave);
        }
        foreach (Ingredient ingredient in FindObjectsOfType<Ingredient>())
        {
            ingredient.gameObject.SetActive(false);
        }
    }
}
