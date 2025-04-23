using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnPluxAnim : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] _animPosition;

    private AudioSource sound;


    private void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    private void PlayAnim()
    {
        if(_animPosition != null)
        {
            var playAnimation = _animPosition[UnityEngine.Random.Range(0, _animPosition.Length)]?.GetComponent<Animator>();

            playAnimation.SetTrigger("Plux");

            Debug.Log("Down");
        }
            
        //throw new Exception($"You did not specify the animation spawn point (senior)");
    }  
    private void PlaySund()
    {
        sound.Play();
    }

    private void OnEnable()
    {
        Soup.IOnIngredientAdded += PlayAnim;
        Soup.IOnIngredientAdded += PlaySund;
    }

    private void OnDisable()
    {
        Soup.IOnIngredientAdded -= PlayAnim;
        Soup.IOnIngredientAdded -= PlaySund;
    }
}
