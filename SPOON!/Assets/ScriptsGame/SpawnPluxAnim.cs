using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPluxAnim : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] _animPosition;
    [SerializeField] private Soup _soupGameobject;

    private void PlayAnim()
    {
        if(_animPosition != null)
        {
            var play = _animPosition[UnityEngine.Random.Range(0, _animPosition.Length)]?.GetComponent<Animator>();

            play.SetTrigger("Plux");
        }
            
        throw new Exception($"You did not specify the animation spawn point (senior)");
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }
}
