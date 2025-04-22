using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private int _scene;

    public void SceneReplacement()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(_scene);
    }
}