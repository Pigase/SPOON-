using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class WaveView : MonoBehaviour
{
    [SerializeField] private float _pauseTime = 5;
    [SerializeField] private TextMeshProUGUI _textNumberWave;

    private float _numberWave=1;

    private void OnEnable()
    {
        GameManipulator.NumberWave += UpNumberWave;
    }
    private void OnDisable()
    {
        GameManipulator.NumberWave -= UpNumberWave;
    }
    private void UpNumberWave(int num)
    {
       StartCoroutine(WaitPause(_pauseTime,num));
    }
    private void Update()
    {
        if(_numberWave==4)
        {
            _textNumberWave.text = "BOSS";
        }
        else
        {
            _textNumberWave.text = _numberWave + "/3";
        }
    }
    private IEnumerator WaitPause(float pauseTime,int num)
    {
        yield return new WaitForSeconds(pauseTime);
        _numberWave = num;
    }
}
