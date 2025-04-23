using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpoonControler : MonoBehaviour
{
    [SerializeField] private GameObject _rightObgect;
    [SerializeField] private GameObject _upObgect;
    [SerializeField] private float _Y;
    [SerializeField] private float _X;

    private Vector2 _tempMove;
    private Vector2 pos;
    private bool playerLive = true;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        _Y = _upObgect.transform.position.y;
        _X = _rightObgect.transform.position.x;
    }
    private void Update()
    {
        if (Time.deltaTime <= 0f)
            return;
        if (playerLive)
        {
            Click();
            Barier();
            CheckForFlip();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Ingredient ingredient = other.GetComponent<Ingredient>();
        if (ingredient == null)
            return;

        PlaySound();
        other.gameObject.SetActive(false); // ��������� ����������
    }
    private void Barier()
    {
        if (transform.position.x > _X + 2)
            transform.position = new Vector2(_X + 2, transform.position.y);//������� � ����� ��� ��� ����� ����� �����
        if (transform.position.x < -_X)
            transform.position = new Vector2(-_X, transform.position.y);
        if (transform.position.y > _Y - 5)
            transform.position = new Vector2(transform.position.x, _Y - 5);
        if (transform.position.y < -_Y)
            transform.position = new Vector2(transform.position.x, -_Y);

    }
    private void Click()
    {
        if (Input.GetMouseButtonDown(0))
            _tempMove = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
            Vector2 touchPos1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.y = transform.position.y - (_tempMove.y - touchPos1.y);
            pos.x = transform.position.x - (_tempMove.x - touchPos1.x);
            transform.position = pos;
            _tempMove = touchPos1;
        }

    }
    private void CheckForFlip()
    {
        if(transform.position.x < 0)
        {
            gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if (transform.position.x > 2)
        {
            gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    private void PlaySound()
    {
        audioSource?.Play();
    }
}
