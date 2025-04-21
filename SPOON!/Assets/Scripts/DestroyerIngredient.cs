using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerIngredient : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ingredient"))
        {
            other.gameObject.SetActive(false); // ���������� ����������
        }
    }
}
