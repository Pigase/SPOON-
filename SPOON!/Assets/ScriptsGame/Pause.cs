using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public void PauseGaame()
    {
        Time.timeScale = 0f;
    }

    public void ContinueGaame()
    {
        Time.timeScale = 1.0f;
    }
}
