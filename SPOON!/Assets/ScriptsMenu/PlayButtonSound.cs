using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonSound : MonoBehaviour
{
    [SerializeField] private AudioSource audio;

    public void PlaySound()
    {
        audio.Play();
    }
}
