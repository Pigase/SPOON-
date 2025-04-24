using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GameManipulator : MonoBehaviour
{
    [SerializeField] private int _numberWave=1;
    [SerializeField] private float _bossWait=5f;
    [SerializeField] private GameObject _canvasGameOver;
    [SerializeField] private GameObject _canvasWin;
    [SerializeField] private Boss _boss;
    [SerializeField] private GameObject[] _boy;

    private AudioSource[] audio;

    public static Action NewWave;
    public static Action BossWave;
    public static Action<int> NumberWave;

    private void Start()
    {
        audio = GetComponents<AudioSource>();
    }

    private void OnEnable()
    {
        Soup.OnSoupRuined += GameOverPanel;
        Soup.OnSoupFull += NextWave;
        Boss.Win += WinPanel;
    }
    private void OnDisable()
    {
        Soup.OnSoupRuined -= GameOverPanel;
        Soup.OnSoupFull -= NextWave;
        Boss.Win -= WinPanel;
    }

    private void GameOverPanel()
    {
        Time.timeScale = 0f;
        _canvasGameOver.SetActive(true);

        audio[1].Stop();   
    }
    private void WinPanel()
    {
        Time.timeScale = 0f;
        _canvasWin.SetActive(true);

        audio[1].Stop();
    }
    private void NextWave()
    {
        if (_numberWave == 1)
        {
            NewWave?.Invoke();
            _numberWave++;
            _boy[0]?.GetComponent<Animator>()?.SetTrigger("OneWave");
            NumberWave.Invoke(_numberWave);
        }
        else if (_numberWave == 2)
        {
            _boy[1]?.GetComponent<Animator>()?.SetTrigger("TwoWave");
            NewWave?.Invoke();
            _numberWave++;
            NumberWave.Invoke(_numberWave);
        }
        else if (_numberWave == 3)
        {
            _boy[2]?.GetComponent<Animator>()?.SetTrigger("ThreeWave");
            BossWave?.Invoke();
            _numberWave++;
            NumberWave.Invoke(_numberWave);
            StartCoroutine(PauseSpawningBoss(_bossWait));
        }
        foreach (Ingredient ingredient in FindObjectsOfType<Ingredient>())
        {
            ingredient.gameObject.SetActive(false);
        }
    }
    private IEnumerator PauseSpawningBoss(float pauseTime)
    {
        yield return new WaitForSeconds(pauseTime);
        _boss.gameObject.SetActive(true);
    }
}
